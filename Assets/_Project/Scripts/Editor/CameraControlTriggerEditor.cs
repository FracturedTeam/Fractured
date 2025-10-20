using UnityEditor;
using UnityEngine;
using Unity.Cinemachine;

#if UNITY_EDITOR
[CustomEditor(typeof(CameraControlTrigger), true), CanEditMultipleObjects]
public class CameraControlTriggerEditor : Editor {
    SerializedProperty cameraControlTrigger;

    private SerializedProperty cameraOnLeftProp;
    private SerializedProperty cameraOnRightProp;
    private SerializedProperty cameraOnFrontProp;
    private SerializedProperty cameraOnBackProp;
    
    private bool showSwapSettings = false;
    
    private void OnEnable() {
        cameraControlTrigger = serializedObject.FindProperty("customInspectorObjects");

        if (cameraControlTrigger != null) {
            cameraOnLeftProp = cameraControlTrigger.FindPropertyRelative("cameraOnLeft");
            cameraOnRightProp = cameraControlTrigger.FindPropertyRelative("cameraOnRight");
            cameraOnFrontProp = cameraControlTrigger.FindPropertyRelative("cameraOnFront");
            cameraOnBackProp = cameraControlTrigger.FindPropertyRelative("cameraOnBack");
        }
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        
        DrawPropertiesExcluding(serializedObject, "customInspectorObjects");
        
        EditorGUILayout.Space();
        
        if (cameraControlTrigger == null)
        {
            EditorGUILayout.HelpBox("Propriété customInspectorObjects introuvable", MessageType.Error);
            serializedObject.ApplyModifiedProperties();
            return;
        }
        
        EditorGUILayout.Space();
        
        showSwapSettings = EditorGUILayout.BeginFoldoutHeaderGroup(showSwapSettings, "Camera Swap Settings");
        EditorGUILayout.Space();

        if (showSwapSettings) {
            GUI.color = Color.deepPink;
            EditorGUILayout.PropertyField(cameraOnLeftProp, new GUIContent("Camera On Left"));
            GUI.color = Color.limeGreen;
            EditorGUILayout.PropertyField(cameraOnRightProp, new GUIContent("Camera On Right"));
            GUI.color = Color.cadetBlue;
            EditorGUILayout.PropertyField(cameraOnFrontProp, new GUIContent("Camera On Front"));
            GUI.color = Color.darkOrange;
            EditorGUILayout.PropertyField(cameraOnBackProp, new GUIContent("Camera On Back"));
            GUI.color = Color.white;
        }
        
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
