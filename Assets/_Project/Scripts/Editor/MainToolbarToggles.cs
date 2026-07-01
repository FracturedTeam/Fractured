using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

namespace _Project.Scripts.Editor {
    public class MainToolbarToggles {
        
        [MainToolbarElement("PlayMode/PlayWithPersistent", defaultDockPosition = MainToolbarDockPosition.Middle)]
        public static MainToolbarToggle PlayWithPersistentScene() {
            var currentValue = DefaultSceneLoader.playWithPersistent;
            var onIcon = EditorGUIUtility.IconContent("animationvisibilitytoggleon").image as Texture2D;
            var offIcon = EditorGUIUtility.IconContent("animationvisibilitytoggleoff").image as Texture2D;
            var icon = currentValue ? onIcon : offIcon;
            var content = new MainToolbarContent(icon, "Persistent");
            
            return new MainToolbarToggle(content, currentValue, OnToggleValueChanged);
        }
        
        private static void OnToggleValueChanged(bool newValue) {
            DefaultSceneLoader.playWithPersistent = newValue;
            MainToolbar.Refresh("PlayMode/PlayWithPersistent");
        }

    }
}