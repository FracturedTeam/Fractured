using System;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TriggerComponent : MonoBehaviour
{
    public bool showHideList = false; 
  // [ShowIf(ActionOnConditionFail.JustDisable, ConditionOperator.And, nameof(showHideList))]
    [SerializeField] private UnityEvent OnInteract;
    
    [SerializeField, ] internal UnityEvent OnCollisionTriggerEnter;
    [SerializeField, ] private UnityEvent OnCollisionTriggerExit;
    
    [SerializeField, ] private UnityEvent OnHideReveal;
    
    [SerializeField, ] private UnityEvent OnTextHideReveal;
    
    [SerializeField, ] internal UnityEvent OnInteractSuccess;
    [SerializeField, ] private UnityEvent OnInteractFailed;
    
    [SerializeField, ] internal UnityEvent OnSetStateOn;
    [SerializeField, ] private UnityEvent OnSetStateOff;    
    
    
    [SerializeField, ] internal UnityEvent OnSceneComplete;
    [SerializeField, ] private UnityEvent OnAtelierComplete;
    
}

#if UNITY_EDITOR
public enum ConditionOperator
{
    // A field is visible/enabled only if all conditions are true.
    And,
    // A field is visible/enabled if at least ONE condition is true.
    Or,
}

public enum ActionOnConditionFail
{
    // If condition(s) are false, don't draw the field at all.
    DontDraw,
    // If condition(s) are false, just set the field as disabled.
    JustDisable,
}



#endif
