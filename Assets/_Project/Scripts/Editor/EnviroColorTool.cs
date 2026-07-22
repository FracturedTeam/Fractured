using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class EnviroColorTool : EditorWindow
{
    static int clones = 1;
    static float transition = 0;

    static Color act1_Color_A;
    static Vector2 act1_Color_A_Location = new Vector2(0f,0.5f);
    static Color act1_Color_B;
    static Vector2 act1_Color_B_Location = new Vector2(0.5f, 1f);
    static Color act1_Color_C;

    static Color act2_Color_A;
    static Vector2 act2_Color_A_Location = new Vector2(0f, 0.5f);
    static Color act2_Color_B;
    static Vector2 act2_Color_B_Location = new Vector2(0.5f, 1f);
    static Color act2_Color_C;

    static Color act3_Color_A;
    static Vector2 act3_Color_A_Location = new Vector2(0f, 0.5f);
    static Color act3_Color_B;
    static Vector2 act3_Color_B_Location = new Vector2(0.5f, 1f);
    static Color act3_Color_C;

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

        EditorGUILayout.LabelField("ACT 1 Color A");
        act1_Color_A = EditorGUILayout.ColorField(act1_Color_A);
        act1_Color_A_Location = EditorGUILayout.Vector2Field("ACT 1 Color A Location", act1_Color_A_Location);
        EditorGUILayout.LabelField("ACT 1 Color B");
        act1_Color_B = EditorGUILayout.ColorField(act1_Color_B);
        act1_Color_B_Location = EditorGUILayout.Vector2Field("ACT 1 Color B Location", act1_Color_B_Location);
        EditorGUILayout.LabelField("ACT 1 Color C");
        act1_Color_C = EditorGUILayout.ColorField(act1_Color_C);

        EditorGUILayout.LabelField("ACT 2 Color A");
        act2_Color_A = EditorGUILayout.ColorField(act2_Color_A);
        act2_Color_A_Location = EditorGUILayout.Vector2Field("ACT 2 Color A Location", act2_Color_A_Location);
        EditorGUILayout.LabelField("ACT 2 Color B");
        act2_Color_B = EditorGUILayout.ColorField(act2_Color_B);
        act2_Color_B_Location = EditorGUILayout.Vector2Field("ACT 2 Color B Location", act2_Color_B_Location);
        EditorGUILayout.LabelField("ACT 2 Color C");
        act2_Color_C = EditorGUILayout.ColorField(act2_Color_C);

        EditorGUILayout.LabelField("ACT 3 Color A");
        act3_Color_A = EditorGUILayout.ColorField(act3_Color_A);
        act3_Color_A_Location = EditorGUILayout.Vector2Field("ACT 3 Color A Location", act3_Color_A_Location);
        EditorGUILayout.LabelField("ACT 3 Color B");
        act3_Color_B = EditorGUILayout.ColorField(act3_Color_B);
        act3_Color_B_Location = EditorGUILayout.Vector2Field("ACT 3 Color B Location", act3_Color_B_Location);
        EditorGUILayout.LabelField("ACT 3 Color C");
        act3_Color_C = EditorGUILayout.ColorField(act3_Color_C);


        if (GUILayout.Button("View"))
        {
            Shader.SetGlobalFloat("_ActGlobalTransition", transition);
            Shader.SetGlobalFloat("_CurrentAct", clones);

            Shader.SetGlobalColor("_ACT1_Color_A", act1_Color_A);
            Shader.SetGlobalVector("_ACT1_Color_A_Location", act1_Color_A_Location);
            Shader.SetGlobalColor("_ACT1_Color_B", act1_Color_B);
            Shader.SetGlobalVector("_ACT1_Color_B_Location", act1_Color_B_Location);
            Shader.SetGlobalColor("_ACT1_Color_C", act1_Color_C);

            Shader.SetGlobalColor("_ACT2_Color_A", act2_Color_A);
            Shader.SetGlobalVector("_ACT2_Color_A_Location", act2_Color_A_Location);
            Shader.SetGlobalColor("_ACT2_Color_B", act2_Color_B);
            Shader.SetGlobalVector("_ACT2_Color_B_Location", act2_Color_B_Location);
            Shader.SetGlobalColor("_ACT2_Color_C", act2_Color_C);

            Shader.SetGlobalColor("_ACT3_Color_A", act3_Color_A);
            Shader.SetGlobalVector("_ACT3_Color_A_Location", act3_Color_A_Location);
            Shader.SetGlobalColor("_ACT3_Color_B", act3_Color_B);
            Shader.SetGlobalVector("_ACT3_Color_B_Location", act3_Color_B_Location);
            Shader.SetGlobalColor("_ACT3_Color_C", act3_Color_C);

        }
    }
    
}