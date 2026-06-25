using _Project.Scripts.Player.Camera;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace _Project.Scripts.Editor {
    [CustomEditor(typeof(CameraControlTrigger), true), CanEditMultipleObjects]
    public class CameraControlTriggerEditor : UnityEditor.Editor {
        SerializedProperty cameraControlTrigger;

        private SerializedProperty cameraOnLeftProp;
        private SerializedProperty cameraOnRightProp;
        private SerializedProperty cameraOnFrontProp;
        private SerializedProperty cameraOnBackProp;
        
        private bool showSwapSettings = true;
        
        private void OnEnable() {
            cameraControlTrigger = serializedObject.FindProperty("customInspectorObjects");

            if (cameraControlTrigger == null) return;
            
            cameraOnLeftProp = cameraControlTrigger.FindPropertyRelative("cameraOnLeft");
            cameraOnRightProp = cameraControlTrigger.FindPropertyRelative("cameraOnRight");
            cameraOnFrontProp = cameraControlTrigger.FindPropertyRelative("cameraOnFront");
            cameraOnBackProp = cameraControlTrigger.FindPropertyRelative("cameraOnBack");
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
                GUI.backgroundColor = Color.deepPink;
                EditorGUILayout.PropertyField(cameraOnLeftProp, new GUIContent("Camera On Left"));
                GUI.backgroundColor = Color.limeGreen;
                EditorGUILayout.PropertyField(cameraOnRightProp, new GUIContent("Camera On Right"));
                GUI.backgroundColor = Color.cadetBlue;
                EditorGUILayout.PropertyField(cameraOnFrontProp, new GUIContent("Camera On Front"));
                GUI.backgroundColor = Color.darkOrange;
                EditorGUILayout.PropertyField(cameraOnBackProp, new GUIContent("Camera On Back"));
                GUI.backgroundColor = Color.white;
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
