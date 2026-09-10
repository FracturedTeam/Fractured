using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnviroColorTool : EditorWindow
{
    private static Profil profil;
    
    // static int act = 1;
    // static float transition = 0;

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

    // fragments colors
    static Color FRAG_ACT1_A_BaseColor;
    static Color FRAG_ACT1_A_HighlightColor;
    static Color FRAG_ACT1_B_BaseColor;
    static Color FRAG_ACT1_B_HighlightColor;
    static Color FRAG_ACT1_AB_BaseColor;
    static Color FRAG_ACT1_AB_HighlightColor;

    static Color FRAG_ACT2_A_BaseColor;
    static Color FRAG_ACT2_A_HighlightColor;
    static Color FRAG_ACT2_B_BaseColor;
    static Color FRAG_ACT2_B_HighlightColor;
    static Color FRAG_ACT2_AB_BaseColor;
    static Color FRAG_ACT2_AB_HighlightColor;

    static Color FRAG_ACT3_A_BaseColor;
    static Color FRAG_ACT3_A_HighlightColor;
    static Color FRAG_ACT3_B_BaseColor;
    static Color FRAG_ACT3_B_HighlightColor;
    static Color FRAG_ACT3_AB_BaseColor;
    static Color FRAG_ACT3_AB_HighlightColor;

    bool show1A = true;
    bool show1B = true;
    bool show2A = true;
    bool show2B = true;
    bool show3A = true;
    bool show3B = true;

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
            Debug.Log("create new profil");
        }

        EditorGUILayout.LabelField("Act");
        profil.act = EditorGUILayout.IntSlider(profil.act,1,3); 
        
        EditorGUILayout.LabelField("ActGlobalTransition");
        profil.transition = EditorGUILayout.Slider(profil.transition, 0, 1);
        
        EditorGUILayout.Space(20);
        show1A = EditorGUILayout.BeginFoldoutHeaderGroup(show1A, "Act 1 : Environment");
        if(show1A)
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

        show1B = EditorGUILayout.BeginFoldoutHeaderGroup(show1B, "Act 1 : Glass Fragments");
        if (show1B)
        {
            EditorGUILayout.LabelField("ACT 1 FRAGMENT A Color");
            profil.FRAG_ACT1_A_BaseColor = EditorGUILayout.ColorField(profil.FRAG_ACT1_A_BaseColor);
            EditorGUILayout.LabelField("ACT 1 FRAGMENT A Highlight Color");
            profil.FRAG_ACT1_A_HighlightColor = EditorGUILayout.ColorField(profil.FRAG_ACT1_A_HighlightColor);
            EditorGUILayout.LabelField("ACT 1 FRAGMENT B Color");
            profil.FRAG_ACT1_B_BaseColor = EditorGUILayout.ColorField(profil.FRAG_ACT1_B_BaseColor);
            EditorGUILayout.LabelField("ACT 1 FRAGMENT B Highlight Color");
            profil.FRAG_ACT1_B_HighlightColor = EditorGUILayout.ColorField(profil.FRAG_ACT1_B_HighlightColor);
            EditorGUILayout.LabelField("ACT 1 FRAGMENT AB Color");
            profil.FRAG_ACT1_AB_BaseColor = EditorGUILayout.ColorField(profil.FRAG_ACT1_AB_BaseColor);
            EditorGUILayout.LabelField("ACT 1 FRAGMENT AB Highlight Color");
            profil.FRAG_ACT1_AB_HighlightColor = EditorGUILayout.ColorField(profil.FRAG_ACT1_AB_HighlightColor);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(20);
        show2A = EditorGUILayout.BeginFoldoutHeaderGroup(show2A, "Act 2 : Environment");
        if(show2A)
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

        show2B = EditorGUILayout.BeginFoldoutHeaderGroup(show2B, "Act 2 : Glass Fragments");
        if (show2B)
        {
            EditorGUILayout.LabelField("ACT 2 FRAGMENT A Color");
            profil.FRAG_ACT2_A_BaseColor = EditorGUILayout.ColorField(profil.FRAG_ACT2_A_BaseColor);
            EditorGUILayout.LabelField("ACT 2 FRAGMENT A Highlight Color");
            profil.FRAG_ACT2_A_HighlightColor = EditorGUILayout.ColorField(profil.FRAG_ACT2_A_HighlightColor);
            EditorGUILayout.LabelField("ACT 2 FRAGMENT B Color");
            profil.FRAG_ACT2_B_BaseColor = EditorGUILayout.ColorField(profil.FRAG_ACT2_B_BaseColor);
            EditorGUILayout.LabelField("ACT 2 FRAGMENT B Highlight Color");
            profil.FRAG_ACT2_B_HighlightColor = EditorGUILayout.ColorField(profil.FRAG_ACT2_B_HighlightColor);
            EditorGUILayout.LabelField("ACT 2 FRAGMENT AB Color");
            profil.FRAG_ACT2_AB_BaseColor = EditorGUILayout.ColorField(profil.FRAG_ACT2_AB_BaseColor);
            EditorGUILayout.LabelField("ACT 2 FRAGMENT AB Highlight Color");
            profil.FRAG_ACT2_AB_HighlightColor = EditorGUILayout.ColorField(profil.FRAG_ACT2_AB_HighlightColor);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(20);
        show3A = EditorGUILayout.BeginFoldoutHeaderGroup(show3A, "Act 3 : Environment");
        if(show3A)
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

        show3B = EditorGUILayout.BeginFoldoutHeaderGroup(show3B, "Act 3 : Glass Fragments");
        if (show3B)
        {
            EditorGUILayout.LabelField("ACT 3 FRAGMENT A Color");
            profil.FRAG_ACT3_A_BaseColor = EditorGUILayout.ColorField(profil.FRAG_ACT3_A_BaseColor);
            EditorGUILayout.LabelField("ACT 3 FRAGMENT A Highlight Color");
            profil.FRAG_ACT3_A_HighlightColor = EditorGUILayout.ColorField(profil.FRAG_ACT3_A_HighlightColor);
            EditorGUILayout.LabelField("ACT 3 FRAGMENT B Color");
            profil.FRAG_ACT3_B_BaseColor = EditorGUILayout.ColorField(profil.FRAG_ACT3_B_BaseColor);
            EditorGUILayout.LabelField("ACT 3 FRAGMENT B Highlight Color");
            profil.FRAG_ACT3_B_HighlightColor = EditorGUILayout.ColorField(profil.FRAG_ACT3_B_HighlightColor);
            EditorGUILayout.LabelField("ACT 3 FRAGMENT AB Color");
            profil.FRAG_ACT3_AB_BaseColor = EditorGUILayout.ColorField(profil.FRAG_ACT3_AB_BaseColor);
            EditorGUILayout.LabelField("ACT 3 FRAGMENT AB Highlight Color");
            profil.FRAG_ACT3_AB_HighlightColor = EditorGUILayout.ColorField(profil.FRAG_ACT3_AB_HighlightColor);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();


        if (GUILayout.Button("View"))
        {
            Set();
        }
    }

    private void Awake()
    {
        profil = AssetDatabase.LoadAssetAtPath<Profil>("Assets/_Project/Art/Shaders/ColorProfil.asset");
        if (profil == null)
        {
            profil = ScriptableObject.CreateInstance<Profil>();
            AssetDatabase.CreateAsset(profil, "Assets/_Project/Art/Shaders/ColorProfil.asset");
            Debug.Log("create new profil");
        }
        
        Set();
    }


    private static void Set()
    { 
        profil = AssetDatabase.LoadAssetAtPath<Profil>("Assets/_Project/Art/Shaders/ColorProfil.asset");
        if (profil == null)
        {
            profil = ScriptableObject.CreateInstance<Profil>();
            AssetDatabase.CreateAsset(profil, "Assets/_Project/Art/Shaders/ColorProfil.asset");
            Debug.Log("create new profil");
        }
        
        Shader.SetGlobalFloat("_ActGlobalTransition", profil.transition);
        Shader.SetGlobalFloat("_CurrentAct", profil.act);

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

        Shader.SetGlobalColor("_FRAG_ACT1_A_BaseColor", profil.FRAG_ACT1_A_BaseColor);
        Shader.SetGlobalColor("_FRAG_ACT1_A_HighlightColor", profil.FRAG_ACT1_A_HighlightColor);
        Shader.SetGlobalColor("_FRAG_ACT1_B_BaseColor", profil.FRAG_ACT1_B_BaseColor);
        Shader.SetGlobalColor("_FRAG_ACT1_B_HighlightColor", profil.FRAG_ACT1_B_HighlightColor);
        Shader.SetGlobalColor("_FRAG_ACT1_AB_BaseColor", profil.FRAG_ACT1_AB_BaseColor);
        Shader.SetGlobalColor("_FRAG_ACT1_AB_HighlightColor", profil.FRAG_ACT1_AB_HighlightColor);

        Shader.SetGlobalColor("_FRAG_ACT2_A_BaseColor", profil.FRAG_ACT2_A_BaseColor);
        Shader.SetGlobalColor("_FRAG_ACT2_A_HighlightColor", profil.FRAG_ACT2_A_HighlightColor);
        Shader.SetGlobalColor("_FRAG_ACT2_B_BaseColor", profil.FRAG_ACT2_B_BaseColor);
        Shader.SetGlobalColor("_FRAG_ACT2_B_HighlightColor", profil.FRAG_ACT2_B_HighlightColor);
        Shader.SetGlobalColor("_FRAG_ACT2_AB_BaseColor", profil.FRAG_ACT2_AB_BaseColor);
        Shader.SetGlobalColor("_FRAG_ACT2_AB_HighlightColor", profil.FRAG_ACT2_AB_HighlightColor);

        Shader.SetGlobalColor("_FRAG_ACT3_A_BaseColor", profil.FRAG_ACT3_A_BaseColor);
        Shader.SetGlobalColor("_FRAG_ACT3_A_HighlightColor", profil.FRAG_ACT3_A_HighlightColor);
        Shader.SetGlobalColor("_FRAG_ACT3_B_BaseColor", profil.FRAG_ACT3_B_BaseColor);
        Shader.SetGlobalColor("_FRAG_ACT3_B_HighlightColor", profil.FRAG_ACT3_B_HighlightColor);
        Shader.SetGlobalColor("_FRAG_ACT3_AB_BaseColor", profil.FRAG_ACT3_AB_BaseColor);
        Shader.SetGlobalColor("_FRAG_ACT3_AB_HighlightColor", profil.FRAG_ACT3_AB_HighlightColor);

        EditorUtility.SetDirty(profil);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
    }
}