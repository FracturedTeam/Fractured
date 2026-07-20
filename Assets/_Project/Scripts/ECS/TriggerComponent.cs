using System;
using System.Collections.Generic;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BaseObject))] 
public class TriggerComponent : MonoBehaviour
{
    [SerializeField] internal List<CustomEvent> OnInteract;
 
    [SerializeField, ] internal List<CustomEvent> OnCollisionTriggerEnter;
    [SerializeField, ] internal List<CustomEvent> OnCollisionTriggerExit;

    [SerializeField, ] internal List<CustomEvent> OnHideReveal;
    
    [SerializeField, ] internal List<CustomEvent> OnTextHideReveal;
    
    [SerializeField, ] internal List<CustomEvent> OnInteractSuccess;
    [SerializeField, ] internal List<CustomEvent> OnInteractFailed;
    
    [SerializeField, ] internal List<CustomEvent> OnSetStateOn;
    [SerializeField, ] internal List<CustomEvent> OnSetStateOff;    
    
    [SerializeField, ] internal List<CustomEvent> OnSceneComplete;
    [SerializeField, ] internal List<CustomEvent> OnAtelierComplete;
    
    public void OnFunction(List<CustomEvent> events)
    {
        foreach (var customEvent in events)
        {
            customEvent.Call();
        }
    }

    #region editor
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TriggerComponent))]
    public class TriggerComponentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.BeginChangeCheck();

            /// Label Field
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(position, label);

            /// Property Field
            SerializedProperty unityEventProp = property.FindPropertyRelative("unityEvent");
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            position.height = EditorGUI.GetPropertyHeight(unityEventProp);
            EditorGUI.PropertyField(position, unityEventProp);

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            /// Height of the label
            float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            /// Height of the property
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("unityEvent"));
            return height;
        }
    }
    
#endif 
    
    #endregion
    
}

[Serializable]
public class CustomEvent 
{

    public UnityEvent actions;
    public int times = 1;
    private bool noLimit = true;
    private int count;
    private bool initialized;
    

    public void Call()
    {
        if (!initialized)
        {
            initialized = true;
            count = times;
            noLimit = (times == 0);
        }
        if (!noLimit && count < 1) 
            return;
        
        count--;
        actions.Invoke();
    }

    public void ForceCall()
    {
        actions.Invoke();
    }
}