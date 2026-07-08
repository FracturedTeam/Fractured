using _Project.Scripts.Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TriggerComponent : MonoBehaviour
{
    [HideInInspector] // HideInInspector makes sure the default inspector won't show these fields.
    public bool StartTemp;

    [HideInInspector]
    public InputField iField;

    [HideInInspector]
    public GameObject Template;

    // ... Update(), Awake(), etc
}

#if UNITY_EDITOR
[CustomEditor(typeof(TriggerComponent))]
public class TriggerComponent_Editor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        TriggerComponent script = (TriggerComponent)target;

        // draw checkbox for the bool
        script.StartTemp = EditorGUILayout.Toggle("Start Temp", script.StartTemp);
        if (script.StartTemp) // if bool is true, show other fields
        {
            script.iField = EditorGUILayout.ObjectField("I Field", script.iField, typeof(InputField), true) as InputField;
            script.Template = EditorGUILayout.ObjectField("Template", script.Template, typeof(GameObject), true) as GameObject;
        }

        if (GUILayout.Button("Your ButtonText"))
        {
        }
    }
}

public class EditorGUILayoutEnumPopup : EditorWindow
{
    public TutorialTriggerType op;
    [MenuItem("Examples/Editor GUILayout Enum Popup usage")]
    static void Init()
    {
        UnityEditor.EditorWindow window = GetWindow(typeof(EditorGUILayoutEnumPopup));
        window.Show();
    }

    void OnGUI()
    {
        op = (TutorialTriggerType)EditorGUILayout.EnumPopup("Primitive to create:", op);
        if (GUILayout.Button("Create"))
            InstantiatePrimitive(op);
        UnityEditor.EditorWindow window = GetWindow(typeof(EditorGUILayoutEnumPopup));
        window.Show();
    }

    void Open()
    {
        
    }

    void InstantiatePrimitive(TutorialTriggerType op)
    {
        switch (op)
        {
            case TutorialTriggerType.None:
            case TutorialTriggerType.OnSuccess:
            case TutorialTriggerType.OnInteract:
            case TutorialTriggerType.OnHideReveal:
            case TutorialTriggerType.OnLeavingMemory:
            case TutorialTriggerType.OnUnsolved:
            case TutorialTriggerType.OnCanBeSeen:
            default:
                Debug.LogError("Unrecognized Option");
                break;
        }
    }
    
    
}

#endif
