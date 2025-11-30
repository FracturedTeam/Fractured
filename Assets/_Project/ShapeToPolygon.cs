
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class ShapeToPolygon : MonoBehaviour
{
    [SerializeField] MeshFilter _target;
    [SerializeField] bool _useMesh;
    [SerializeField] private int _refiningIterations = 5;
    [SerializeField] private int _pointCount = 100;
    [SerializeField] private Vector3 _boundingBox = Vector3.one;
    [SerializeField] private SolutionType _solutionType;

    [Header("Visualization")]
    [SerializeField] private float _pointSize = 0.05f;
    [SerializeField] private bool _showNormals;
    [SerializeField] private bool _showPoints;
    [SerializeField] private bool _showOnlyFinalPolygon;
    [SerializeField] private float _normalSize = 0.5f;



    private List<Vector3> _positions = new();
    private List<Vector2> _polygon = new();
    private List<Line> _linesToDraw = new();
    private List<Line2D> _linesToDraw2D = new();
    private List<Vector2> _pointsToDraw2D = new();

    private Camera _camera;
    private Vector3 _pastCamPos;
    private Quaternion _pastCamRot;

    private void OnEnable()
    {
        EditorApplication.update += TryReproject;
    }

    private void OnDisable()
    {
        EditorApplication.update -= TryReproject;
    }

    private void TryReproject()
    {
        if (_pastCamPos != Camera.main.transform.position || _pastCamRot != Camera.main.transform.rotation)
        {
            Reproject();
        }
        _pastCamPos = Camera.main.transform.position;
        _pastCamRot = Camera.main.transform.rotation;

    }

    private void Reproject()
    {
        ClearLinesToDraw();
        MakePolygon();
    }

    public void Generate3DPoints()
    {
        if (_useMesh) ExtractPointsFromMesh();
        else GenerateRandomPoints();
        Reproject();
    }
    private void ExtractPointsFromMesh()
    {
        _positions.Clear();
        if (_target  == null) return;
        foreach (Vector3 vert in _target.sharedMesh.vertices)
        {
            _positions.Add(_target.transform.localToWorldMatrix * new Vector4(vert.x, vert.y, vert.z, 1));
        }
    }
    private void GenerateRandomPoints()
    {
        _positions.Clear();
        for (int i = 0; i < _pointCount; i++)
        {
            Vector3 newPoint = new Vector3(
                                        Random.Range(-_boundingBox.x, _boundingBox.x),
                                        Random.Range(-_boundingBox.y, _boundingBox.y),
                                        Random.Range(-_boundingBox.z, _boundingBox.z));
            _positions.Add(newPoint);
        }
    }

    public void MakePolygon()
    {
        ClearLinesToDraw ();
        _camera = Camera.main;
        FilterVertices();
        _polygon = PositionsTo2D();

        SortPolygonByPolarCoord();
        for (int i = 0; i < _refiningIterations; i++)
        {
            RemoveUselessPoints();
        }
        float offset = 0.0f;
        for (int i = 0; i < _polygon.Count; i++)
        {
            _linesToDraw2D.Add(new Line2D(_polygon[i], _polygon[(i+1)%_polygon.Count], Color.green));
            //_linesToDraw2D.Add(new Line2D(new Vector2(0,0), new Vector2(1,1), Color.yellow));
        }
    }

    private void FilterVertices()
    {
        HashSet<Vector3> myVerts = _positions.ToHashSet();
        _positions = myVerts.ToList();
    }

    private List<Vector2> PositionsTo2D()
    {
        _pointsToDraw2D.Clear();
        List<Vector2> points = new List<Vector2>(); 
        foreach(Vector3 pos in _positions)
        {
            points.Add(ProjectPoint(pos));
            _pointsToDraw2D.Add(ProjectPoint(pos));
        }
        return points;
    }

    private void SortPolygonByPolarCoord()
    {
        _polygon = _polygon.OrderBy((pos) => GetPolarCoord(pos).x).ToList();
    }

    public void ClearLinesToDraw()
    {
        _linesToDraw.Clear();
        _linesToDraw2D.Clear();
    }

    private void RemoveUselessPoints()
    {
        List<int> toRemove = new List<int>();
        
        for (int i = 0; i < _polygon.Count; i++)
        {
            int currIndex = (i + 0) % _polygon.Count;
            Vector2 curr = _polygon[currIndex]; 
            Vector2 prev = _polygon[(i - 1 + _polygon.Count) % _polygon.Count];
            Vector2 next = _polygon[(i + 1) % _polygon.Count];
            var polarPrev = GetPolarCoord(prev);
            var polarCurr = GetPolarCoord(curr);
            var polarNext = GetPolarCoord(next);

            Vector2 prev2next = next - prev;
            Vector2 prev2curr = curr - prev;
            Vector2 normal = new Vector2(-prev2next.y, prev2next.x);
            if (!_showOnlyFinalPolygon) _linesToDraw2D.Add(new Line2D(prev, next, Color.yellow));
            if (!_showOnlyFinalPolygon) _linesToDraw2D.Add(new Line2D(curr, curr + normal.normalized *_normalSize, Color.green));
            normal.Normalize();
            float det = Vector2.Dot(prev2curr.normalized, normal.normalized);

            var cross = Vector3.Cross(prev2next, prev2curr);

          


            var p = _camera.WorldToViewportPoint(_target.transform.position);
            Vector2 center = new Vector2(p.x, p.y);



            // ########## Solution
            switch (_solutionType)
            {
                case ( SolutionType.Cross):
                    if (cross.z > 0) toRemove.Add(currIndex);
                    break;
                case (SolutionType.Dot):
                    if (det > 0) toRemove.Add(currIndex);
                    break;

                case (SolutionType.None):
                default:
                    break;

            }
            

        }
        for (int i = 0; i < toRemove.Count; i++)
        {
            int index = (toRemove[i] - i + _polygon.Count) %_polygon.Count;
            Debug.Log("" + toRemove[i] + " with polygonCount '" + _polygon.Count + "' and i=" + i+ " gives " + index);
            _polygon.RemoveAt(index);
        }
    }

    private Vector3 To3D(Vector2 point) => new Vector3(point.x, point.y, 0);

    private Vector2 ProjectPoint(Vector3 point)
    {
        Vector3 p;
        p = _camera.WorldToViewportPoint(point);
        return new Vector2(p.x, p.y);
    }

    private Vector2 GetPolarCoord(Vector2 pos)
    {
        var p = _camera.WorldToViewportPoint(_target.transform.position);
        pos = pos - new Vector2(p.x, p.y);
        float theta = Mathf.Atan2(pos.y, pos.x);
        float radius = Mathf.Sqrt(Mathf.Pow(pos.x, 2) + Mathf.Pow(pos.y, 2));
        return new Vector2(theta, radius);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.yellow;

        if (_showNormals) foreach(Line line in _linesToDraw) line.Draw();
        foreach(Line2D line in _linesToDraw2D) line.Draw();
        foreach(Vector2 pt in _pointsToDraw2D) Gizmos.DrawSphere(Camera.main.ViewportToWorldPoint(new Vector3(pt.x, pt.y, 1f)), 0.01f);

        foreach (var point in _positions)
        {
            Vector2 pp = ProjectPoint(point);
            pp = pp ;
            float a = GetPolarCoord(pp).x;
            a += Mathf.PI;
            a /= Mathf.PI*2;
            //a = a / (Mathf.PI / 2);
            
            UnityEngine.Color col = new UnityEngine.Color(a,a,a); 
            Gizmos.color = col;
            if (_showPoints) Gizmos.DrawSphere(point, _pointSize);
        }


        if (_polygon != null && _polygon.Count != 0)
        {
            Gizmos.color = UnityEngine.Color.blue;
            foreach (var point in _polygon)
            {
                if (_showPoints) Gizmos.DrawSphere(new Vector3(point.x, 0, point.y), _pointSize);
            }
            /*
            for (int i = 0; i < _polygon.Count; i++)
            {
                Gizmos.DrawLine(new Vector3(_polygon[i].x, 0, _polygon[i].y), new Vector3(_polygon[(i + 1) % _polygon.Count].x, 0, _polygon[(i + 1) % _polygon.Count].y));
            }*/
        }

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(_target.transform.position, _pointSize);

    }

    private struct Line
    {
        public Vector3 A;
        public Vector3 B;
        public Color Col;
        public Line(Vector2 a, Vector2 b, Color col)
        {

            A = new Vector3(a.x, 0, a.y);
            B = new Vector3(b.x, 0, b.y);
            Col = col;
        }
        public void Draw()
        {
            Gizmos.color = Col;
            Gizmos.DrawLine(A, B);
        }
    }

    private struct Line2D
    {
        public Vector2 A;
        public Vector2 B;
        public Color Col;
        public Line2D(Vector2 a, Vector2 b, Color col)
        {

            A =a;
            B = b;
            Col = col;
        }
        public void Draw()
        {
            Gizmos.color = Col;
            float distance = 10f;
            Vector3 Aws = Camera.main.ViewportToWorldPoint(new Vector3(A.x, A.y, 1f));
            Vector3 Bws = Camera.main.ViewportToWorldPoint(new Vector3(B.x, B.y, 1f));
            Gizmos.DrawLine(Aws, Bws);
        }
    }

    private enum SolutionType
    {
        None,
        Cross,
        Dot,
    }
}
