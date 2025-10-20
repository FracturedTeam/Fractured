using Unity.Cinemachine;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
[SaveDuringPlay]
[AddComponentMenu("Cinemachine/Extensions/Camera Lock")]
public class CameraLock : CinemachineExtension
{
    [Header("Lock Settings")]
    public bool lockY = false;
    public float lockYRotation = 0f;
    public bool lockX = false;
    public float lockXRotation = 0f;
    
    [Header("Clamp Settings")]
    public bool clampX = false;
    [Tooltip("Minimum X rotation angle")]
    public float minX = -30f;
    [Tooltip("Maximum X rotation angle")]
    public float maxX = 60f;


    [HideInInspector] public float currentRotation;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime) {
        if (stage != CinemachineCore.Stage.Aim) return;

        if (clampX) {
            var euler = state.RawOrientation.eulerAngles;
            if (euler.x > 180f) euler.x -= 360f;

            currentRotation = euler.x;

            euler.x = Mathf.Clamp(euler.x, minX, maxX);
            state.RawOrientation = Quaternion.Euler(euler);
        }

        if (lockY) {
            var euler = state.RawOrientation.eulerAngles;

            euler.y = lockYRotation;
            state.RawOrientation = Quaternion.Euler(euler);
        }
        
        if (lockX) {
            var euler = state.RawOrientation.eulerAngles;

            euler.x = lockXRotation;
            state.RawOrientation = Quaternion.Euler(euler);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CameraLock))]
public class CameraLockEditor : Editor
{
    private const float barHeight = 10f;

    public override void OnInspectorGUI()
    {
        CameraLock clamp = (CameraLock)target;

        EditorGUILayout.LabelField("Lock Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        
        if(clamp.lockX != true)
            clamp.clampX = EditorGUILayout.Toggle("Clamp X", clamp.clampX);
        
        clamp.lockY = EditorGUILayout.Toggle("Lock Y", clamp.lockY);
        
        if(clamp.clampX != true)
            clamp.lockX = EditorGUILayout.Toggle("Lock X", clamp.lockX);
        
        EditorGUILayout.Space();
        
        if (clamp.clampX) {
            EditorGUILayout.LabelField("Clamp X Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            clamp.minX = EditorGUILayout.FloatField("min X", clamp.minX);
            clamp.maxX = EditorGUILayout.FloatField("max X", clamp.maxX);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }
        
        if (clamp.lockX &&  !clamp.lockY) {
            EditorGUILayout.LabelField("Lock Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            clamp.lockXRotation = EditorGUILayout.FloatField("Lock X Rotation", clamp.lockXRotation);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }
        
        if (clamp.lockY && !clamp.lockX) {
            EditorGUILayout.LabelField("Lock Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            clamp.lockYRotation = EditorGUILayout.FloatField("Lock Y Rotation", clamp.lockYRotation);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }
        
        if (clamp.lockY && clamp.lockX) {
            EditorGUILayout.LabelField("Lock Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            clamp.lockYRotation = EditorGUILayout.FloatField("Lock Y Rotation", clamp.lockYRotation);
            clamp.lockXRotation = EditorGUILayout.FloatField("Lock X Rotation", clamp.lockXRotation);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        // Limite de cohérence
        if (clamp.minX > clamp.maxX)
            EditorGUILayout.HelpBox("min X doit être inférieur à max X", MessageType.Warning);

        // Affichage du pitch actuel (en Play Mode seulement)
        if (Application.isPlaying) {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField($"Current Rotation: {clamp.currentRotation:F1}°");

            // Barre de progression visuelle
            float normalized = Mathf.InverseLerp(clamp.minX, clamp.maxX, clamp.currentRotation);
            Rect rect = GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth, barHeight);
            EditorGUI.DrawRect(rect, Color.gray * 0.4f);
            Rect fillRect = new Rect(rect.x, rect.y, rect.width * Mathf.Clamp01(normalized), rect.height);
            EditorGUI.DrawRect(fillRect, Color.Lerp(Color.red, Color.green, normalized));
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(clamp);
        }
    }
}
#endif
