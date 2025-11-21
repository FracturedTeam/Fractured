using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.SceneManagement;
using System.Linq;
using System.Threading.Tasks;

public static class SetPolygonCollider3D
{
    private static float _deltaAngle = 5f;
    
    
    [MenuItem("Tools/Generate Polygon Colliders %t", false, -1)]
    private static void GenerateCollider()
    {
        var objList = Selection.objects.OfType<GameObject>().ToArray();
        
        PolygonCollider2D polygonCollider2D = objList[0].GetComponent<PolygonCollider2D>();
        
        MeshFilter meshFilter = objList[1].GetComponent<MeshFilter>();
        
        if(meshFilter == null) return;
        
        MeshCollider meshCollider = meshFilter.gameObject.GetComponent<MeshCollider>();

        // ScanCircle(meshFilter, polygonCollider2D, meshCollider);
        AsyncScanCircle(meshFilter, polygonCollider2D, meshCollider);
    }
    
    // Delay for next frame
    private static async void AsyncScanCircle(MeshFilter meshFilter, PolygonCollider2D polygonCollider2D, MeshCollider meshCollider)
    {
        Vector3 previousPosition = meshFilter.transform.position;
        meshFilter.transform.position = Vector3.zero;

        await Task.Delay(50);
        ScanCircle(meshFilter, polygonCollider2D, meshCollider);
        await Task.Delay(10);
        
        meshFilter.transform.position = previousPosition;
    }
    
    
    private static void ScanCircle(MeshFilter meshFilter, PolygonCollider2D polygonCollider2D, MeshCollider meshCollider)
    {
        var points = new List<Vector2>();
        var vertices = meshFilter.sharedMesh.vertices;
        for (var i = 0; i < vertices.Length; i++) 
        {
            points.Add(new Vector2(vertices[i].x, vertices[i].y));
        }

        Vector3 centroid = FindCentroid(points);
        float radius = GetMaxDistance(points, centroid) + 0.01f;
            
            
        List<Vector2> collPoints = new List<Vector2>();
        float angle = 0;
        do
        {
            var pointInCircle = GetPointOnCircle(radius, centroid, angle);
            Vector3 hitPoint;
            if (CheckCollision(meshFilter, pointInCircle, centroid - pointInCircle, out hitPoint))
            {
                collPoints.Add(hitPoint);
            }
            angle += _deltaAngle;
        } while (angle < 360);
            
            
        // collPoints = Sort(Filter(collPoints));
        polygonCollider2D.points = collPoints.ToArray();
    }
    
    
    private static bool CheckCollision(MeshFilter meshFilter, Vector3 origin, Vector3 direction, out Vector3 hitPoint)
    {
        hitPoint = Vector3.zero;
        
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter not assigned.");
            return false;
        }

        Mesh mesh = meshFilter.sharedMesh;
        if (mesh == null)
        {
            Debug.LogError("Mesh not found.");
            return false;
        }

        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;

            for (int i = 0; i < triangles.Length; i += 3)
            {
                Vector3 v0 = vertices[triangles[i]];
                Vector3 v1 = vertices[triangles[i + 1]];
                Vector3 v2 = vertices[triangles[i + 2]];

                if (RayTriangleIntersection(origin, direction, v0, v1, v2))
                {
                    hitPoint = hit.point;
                    return true;
                }
            }
        }
        
        return false;
    }

    // Ray-triangle intersection test
    private static bool RayTriangleIntersection(Vector3 orig, Vector3 dir, Vector3 v0, Vector3 v1, Vector3 v2)
    {
        Vector3 edge1 = v1 - v0;
        Vector3 edge2 = v2 - v0;
        Vector3 pVec = Vector3.Cross(dir, edge2);
        float det = Vector3.Dot(edge1, pVec);

        if (det < Mathf.Epsilon) return false;

        Vector3 tVec = orig - v0;
        float u = Vector3.Dot(tVec, pVec);
        if (u < 0 || u > det) return false;

        Vector3 qVec = Vector3.Cross(tVec, edge1);
        float v = Vector3.Dot(dir, qVec);
        if (v < 0 || u + v > det) return false;

        return true;
    }
    private static Vector2 FindCentroid(List<Vector2> points)
    {
        Vector2 centroid = Vector2.zero;
                
        foreach (var point in points)
        {
            centroid += point;
        }
        centroid /= points.Count;
        return centroid;
    }

    private static float GetMaxDistance(List<Vector2> points, Vector2 centroid)
    {
        float maxDistance = 0;
        foreach (var point in points)
        {
            maxDistance = Mathf.Max(maxDistance, Vector2.Distance(point, centroid));
        }

        return maxDistance;
    }
    
    private static Vector3 GetPointOnCircle(float radius, Vector2 center, float angleDegrees)
    {
                
        // Convert angle to radians
        float angleRadians = angleDegrees * Mathf.Deg2Rad;

        // Calculate x and y coordinates
        float x = center.x + radius * Mathf.Cos(angleRadians);
        float y = center.y + radius * Mathf.Sin(angleRadians);

        var point = new Vector3(x, y, 0);
        return point;
    }
}