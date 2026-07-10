using System;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(BaseObject))] 
public class TriggerComponent : MonoBehaviour
{
    [SerializeField] internal UnityEvent OnInteract;
    
    [SerializeField, ] internal UnityEvent OnCollisionTriggerEnter;
    [SerializeField, ] internal UnityEvent OnCollisionTriggerExit;
    
    [SerializeField, ] internal UnityEvent OnHideReveal;
    
    [SerializeField, ] internal UnityEvent OnTextHideReveal;
    
    [SerializeField, ] internal UnityEvent OnInteractSuccess;
    [SerializeField, ] internal UnityEvent OnInteractFailed;
    
    [SerializeField, ] internal UnityEvent OnSetStateOn;
    [SerializeField, ] internal UnityEvent OnSetStateOff;    
    
    [SerializeField, ] internal UnityEvent OnSceneComplete;
    [SerializeField, ] internal UnityEvent OnAtelierComplete;
    
}