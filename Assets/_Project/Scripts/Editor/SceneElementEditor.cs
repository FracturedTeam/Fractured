using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects.InteractableObjects;
using UnityEditor;

namespace _Project.Scripts.Editor {
    [CustomEditor(typeof(SceneElement))]
    public class SceneElementEditor : UnityEditor.Editor {

        public override void OnInspectorGUI() {
            var scene = (SceneElement)target;
            
            EditorGUILayout.LabelField("Validation Type", EditorStyles.boldLabel);
            scene.validationMethod = (SceneElement.ValidationMethod)EditorGUILayout.EnumPopup("Validation Method", scene.validationMethod);
            EditorGUILayout.Space();

            if (scene.validationMethod is SceneElement.ValidationMethod.Position) {
                EditorGUILayout.LabelField("Position Validation", EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                
                scene.requestedPosition = EditorGUILayout.Vector3Field("Position", scene.requestedPosition);
                
                EditorUtility.SetDirty(target);
                
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
            else if (scene.validationMethod is SceneElement.ValidationMethod.GlassState) {
                EditorGUILayout.LabelField("Glass State Validation", EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                
                scene.requestedVisibility = EditorGUILayout.Toggle("Visibility", scene.requestedVisibility);
                
                EditorUtility.SetDirty(target);
                
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
            else if (scene.validationMethod is SceneElement.ValidationMethod.UseState) {
                EditorGUILayout.LabelField("Use State Validation", EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                
                scene.requestedUseState = EditorGUILayout.Toggle("Use State", scene.requestedUseState);
                
                EditorUtility.SetDirty(target);
                
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }
    }
}