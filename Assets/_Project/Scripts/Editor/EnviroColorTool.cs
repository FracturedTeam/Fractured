using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class EnviroColorTool : EditorWindow
{
    private static Profil profil;
    
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
    
    bool show1 = true;
    bool show2 = true;
    bool show3 = true;

    [MenuItem("Window/EnviroColor")]
    private static void ShowWindow()
    {
        GetWindow<EnviroColorTool>().Show();
    }
    
    void OnGUI()
    {
        Profil profil = AssetDatabase.LoadAssetAtPath<Profil>("Assets/_Project/Art/Shaders/ColorProfil.asset");
        if (profil == null)
        {
            profil = ScriptableObject.CreateInstance<Profil>();
            AssetDatabase.CreateAsset(profil, "Assets/_Project/Art/Shaders/ColorProfil.asset");
        }
        
        EditorGUILayout.LabelField("Act");
        profil.clones = EditorGUILayout.IntSlider(profil.clones,1,3); 
        
        EditorGUILayout.LabelField("ActGlobalTransition");
        profil.transition = EditorGUILayout.Slider(profil.transition, 0, 1);
        
        EditorGUILayout.Space(20);
        show1 = EditorGUILayout.BeginFoldoutHeaderGroup(show1, "Act 1 Parameters");
        
        if(show1)
        {
            EditorGUILayout.LabelField("ACT 1 Color A");
            profil.act1_Color_A = EditorGUILayout.ColorField(profil.act1_Color_A);
            profil.act1_Color_A_Location = EditorGUILayout.Vector2Field("ACT 1 Color A Location", profil.act1_Color_A_Location);
            EditorGUILayout.LabelField("ACT 1 Color B");
            profil.act1_Color_B = EditorGUILayout.ColorField(profil.act1_Color_B);
            profil.act1_Color_B_Location = EditorGUILayout.Vector2Field("ACT 1 Color B Location", profil.act1_Color_B_Location);
            EditorGUILayout.LabelField("ACT 1 Color C");
            profil.act1_Color_C = EditorGUILayout.ColorField(profil.act1_Color_C);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(20);
        show2 = EditorGUILayout.BeginFoldoutHeaderGroup(show2, "Act 2 Parameters");
        
        if(show2)
        {
            EditorGUILayout.LabelField("ACT 2 Color A");
            profil.act2_Color_A = EditorGUILayout.ColorField(profil.act2_Color_A);
            profil.act2_Color_A_Location = EditorGUILayout.Vector2Field("ACT 2 Color A Location", profil.act2_Color_A_Location);
            EditorGUILayout.LabelField("ACT 2 Color B");
            profil.act2_Color_B = EditorGUILayout.ColorField(profil.act2_Color_B);
            profil.act2_Color_B_Location = EditorGUILayout.Vector2Field("ACT 2 Color B Location", profil.act2_Color_B_Location);
            EditorGUILayout.LabelField("ACT 2 Color C");
            profil.act2_Color_C = EditorGUILayout.ColorField(profil.act2_Color_C);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(20);
        show3 = EditorGUILayout.BeginFoldoutHeaderGroup(show3, "Act 3 Parameters");
        
        if(show3)
        {
            EditorGUILayout.LabelField("ACT 3 Color A");
            profil.act3_Color_A = EditorGUILayout.ColorField(profil.act3_Color_A);
            profil.act3_Color_A_Location = EditorGUILayout.Vector2Field("ACT 3 Color A Location", profil.act3_Color_A_Location);
            EditorGUILayout.LabelField("ACT 3 Color B");
            profil.act3_Color_B = EditorGUILayout.ColorField(profil.act3_Color_B);
            profil.act3_Color_B_Location = EditorGUILayout.Vector2Field("ACT 3 Color B Location", profil.act3_Color_B_Location);
            EditorGUILayout.LabelField("ACT 3 Color C");
            profil.act3_Color_C = EditorGUILayout.ColorField(profil.act3_Color_C);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();


        if (GUILayout.Button("View"))
        {
            Shader.SetGlobalFloat("_ActGlobalTransition", profil.transition);
            Shader.SetGlobalFloat("_CurrentAct", profil.clones);

            Shader.SetGlobalColor("_ACT1_Color_A", profil.act1_Color_A);
            Shader.SetGlobalVector("_ACT1_Color_A_Location", profil.act1_Color_A_Location);
            Shader.SetGlobalColor("_ACT1_Color_B", profil.act1_Color_B);
            Shader.SetGlobalVector("_ACT1_Color_B_Location", profil.act1_Color_B_Location);
            Shader.SetGlobalColor("_ACT1_Color_C", profil.act1_Color_C);

            Shader.SetGlobalColor("_ACT2_Color_A", profil.act2_Color_A);
            Shader.SetGlobalVector("_ACT2_Color_A_Location", profil.act2_Color_A_Location);
            Shader.SetGlobalColor("_ACT2_Color_B", profil.act2_Color_B);
            Shader.SetGlobalVector("_ACT2_Color_B_Location", profil.act2_Color_B_Location);
            Shader.SetGlobalColor("_ACT2_Color_C", profil.act2_Color_C);

            Shader.SetGlobalColor("_ACT3_Color_A", profil.act3_Color_A);
            Shader.SetGlobalVector("_ACT3_Color_A_Location", profil.act3_Color_A_Location);
            Shader.SetGlobalColor("_ACT3_Color_B", profil.act3_Color_B);
            Shader.SetGlobalVector("_ACT3_Color_B_Location", profil.act3_Color_B_Location);
            Shader.SetGlobalColor("_ACT3_Color_C", profil.act3_Color_C);

            EditorUtility.SetDirty(profil);
        }
    }
    
    public class Profil : ScriptableObject
    {
        internal int clones = 1;
        internal float transition = 0;

        internal Color act1_Color_A;
        internal Vector2 act1_Color_A_Location = new Vector2(0f,0.5f);
        internal Color act1_Color_B;
        internal Vector2 act1_Color_B_Location = new Vector2(0.5f, 1f);
        internal Color act1_Color_C;

        internal Color act2_Color_A;
        internal Vector2 act2_Color_A_Location = new Vector2(0f, 0.5f);
        internal Color act2_Color_B;
        internal Vector2 act2_Color_B_Location = new Vector2(0.5f, 1f);
        internal Color act2_Color_C;

        internal Color act3_Color_A;
        internal Vector2 act3_Color_A_Location = new Vector2(0f, 0.5f);
        internal Color act3_Color_B;
        internal Vector2 act3_Color_B_Location = new Vector2(0.5f, 1f);
        internal Color act3_Color_C;
    }
}