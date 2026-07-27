
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace _Project.Scripts.Editor {
    [CustomEditor(typeof(DoorInteractable))]
    public class DoorInteractableEditor : UnityEditor.Editor {

        SerializedProperty sceneToLoad;
        SerializedProperty levelDesign;
        SerializedProperty positionField;
        SerializedProperty directionField;

        void OnEnable() {
            sceneToLoad = serializedObject.FindProperty("sceneToLoad");
            levelDesign = sceneToLoad.FindPropertyRelative("levelDesign");
            positionField = sceneToLoad.FindPropertyRelative("playerPosition");
            directionField = sceneToLoad.FindPropertyRelative("direction");
        }
        
        public override void OnInspectorGUI() {
            EditorGUILayout.LabelField("Door Type", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Big Door Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            serializedObject.Update();
            EditorGUILayout.PropertyField(levelDesign, true);
            EditorGUILayout.PropertyField(positionField, true);
            EditorGUILayout.PropertyField(directionField, true);
            
            serializedObject.ApplyModifiedProperties();
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }
    }
}


#endif