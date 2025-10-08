using UnityEditor;
using UnityEngine;
using Unity.Cinemachine;

#if UNITY_EDITOR
[CustomEditor(typeof(CameraControlTrigger), true), CanEditMultipleObjects]
public class CameraControlTriggerEditor : Editor {
    SerializedProperty cameraControlTrigger;

    SerializedProperty swapCameraProp;
    private SerializedProperty panCameraOnContactProp;

    private SerializedProperty cameraOnLeftProp;
    private SerializedProperty cameraOnRightProp;

    private SerializedProperty panDirectionProp;
    private SerializedProperty panDistanceProp;
    private SerializedProperty panTimeProp;
    
    private void OnEnable() {
        cameraControlTrigger = serializedObject.FindProperty("customInspectorObjects");

        if (cameraControlTrigger != null) {
            swapCameraProp = cameraControlTrigger.FindPropertyRelative("swapCamera");
            panCameraOnContactProp = cameraControlTrigger.FindPropertyRelative("panCameraOnContact");

            cameraOnLeftProp = cameraControlTrigger.FindPropertyRelative("cameraOnLeft");
            cameraOnRightProp = cameraControlTrigger.FindPropertyRelative("cameraOnRight");

            panDirectionProp = cameraControlTrigger.FindPropertyRelative("panDirection");
            panDistanceProp = cameraControlTrigger.FindPropertyRelative("panDistance");
            panTimeProp = cameraControlTrigger.FindPropertyRelative("panTime");
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
        
        EditorGUILayout.PropertyField(swapCameraProp, new GUIContent("Swap Camera"));
        EditorGUILayout.PropertyField(panCameraOnContactProp, new GUIContent("Pan Camera"));
        
        EditorGUILayout.Space();

        if (swapCameraProp.boolValue) {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Camera Swap Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(cameraOnLeftProp, new GUIContent("Camera On Left"));
            EditorGUILayout.PropertyField(cameraOnRightProp, new GUIContent("Camera On Right"));
            EditorGUILayout.EndVertical();
        }

        if (panCameraOnContactProp.boolValue) {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Pan Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(panDirectionProp, new GUIContent("Pan Direction"));
            EditorGUILayout.PropertyField(panDistanceProp, new GUIContent("Pan Distance"));
            EditorGUILayout.PropertyField(panTimeProp, new GUIContent("Pan Time"));
            EditorGUILayout.EndVertical();
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
