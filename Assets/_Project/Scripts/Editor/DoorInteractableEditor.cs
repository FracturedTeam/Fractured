
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

        void OnEnable() {
            sceneToLoad = serializedObject.FindProperty("sceneToLoad");
            sceneField = sceneToLoad.FindPropertyRelative("sceneField");
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
                //EditorGUILayout.PropertyField(sceneToLoad, true);
                EditorGUILayout.PropertyField(sceneField, true);
                door.sceneToLoad.playerPosition = EditorGUILayout.Vector3Field("Player Position", door.sceneToLoad.playerPosition);
                door.sceneToLoad.direction = (Direction)EditorGUILayout.EnumPopup("Direction", door.sceneToLoad.direction);
                
                serializedObject.ApplyModifiedProperties();
                
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }
    }
}

#endif