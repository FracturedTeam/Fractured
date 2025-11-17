
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
        SerializedProperty sceneField;
        SerializedProperty positionField;
        SerializedProperty directionField;

        void OnEnable() {
            sceneToLoad = serializedObject.FindProperty("sceneToLoad");
            sceneField = sceneToLoad.FindPropertyRelative("sceneField");
            positionField = sceneToLoad.FindPropertyRelative("playerPosition");
            directionField = sceneToLoad.FindPropertyRelative("direction");
        }
        
        public override void OnInspectorGUI() {
            var door = (DoorInteractable)target;
            
            EditorGUILayout.LabelField("Door Type", EditorStyles.boldLabel);
            door.doorType = (DoorType)EditorGUILayout.EnumPopup("Door Type", door.doorType);
            EditorGUILayout.Space();

            if (door.doorType is DoorType.SmallDoor) {
                EditorGUILayout.LabelField("Small Door Settings", EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                door.exitPoint = (Transform)EditorGUILayout.ObjectField("Exit Point", door.exitPoint, typeof(Transform), true);
                door.linkedDoor = (DoorInteractable)EditorGUILayout.ObjectField("Linked Door", door.linkedDoor, typeof(DoorInteractable), true);
                door.exitDir = (Direction)EditorGUILayout.EnumPopup("Exit Direction", door.exitDir);
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
            else if (door.doorType is DoorType.BigDoor) {
                EditorGUILayout.LabelField("Big Door Settings", EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                
                serializedObject.Update();
                EditorGUILayout.PropertyField(sceneField, true);
                EditorGUILayout.PropertyField(positionField, true);
                EditorGUILayout.PropertyField(directionField, true);
                serializedObject.ApplyModifiedProperties();
                
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }
    }
}

#endif