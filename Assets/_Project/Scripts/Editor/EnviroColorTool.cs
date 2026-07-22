using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class EnviroColorTool : EditorWindow
{
    static int clones = 1;
    static float transition = 1;
    [MenuItem("Window/EnviroColor")]
    private static void ShowWindow()
    {
        GetWindow<EnviroColorTool>().Show();
    }
    
    void OnGUI()
    {
        EditorGUILayout.LabelField("Act");
        clones = EditorGUILayout.IntSlider(clones,1,3); 
        
        EditorGUILayout.LabelField("ActGlobalTransition");
        transition = EditorGUILayout.Slider(transition, 0, 1);
        
        if (GUILayout.Button("View"))
        {
            Shader.SetGlobalFloat("_ActGlobalTransition", transition);
            Shader.SetGlobalFloat("_CurrentAct", clones);
        }
    }
    
}