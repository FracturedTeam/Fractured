using _Project.Scripts.Player.Camera;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace _Project.Scripts.Editor {
    [CustomEditor(typeof(CameraLock))]
    public class CameraLockEditor : UnityEditor.Editor
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
}
#endif
