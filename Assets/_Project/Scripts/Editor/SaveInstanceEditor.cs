using _Project.Scripts.GameServices;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Editor {
    [CustomEditor(typeof(SaveInstance))]
    public class SaveInstanceEditor : UnityEditor.Editor {
        // public override void OnInspectorGUI() {
        //     var saveSystem = (SaveInstance) target;
        //     
        //     DrawDefaultInspector();
        //     
        //     if(GUILayout.Button("Set Interactable"))
        //         saveSystem.SetObjectData();
        // }
    }
}