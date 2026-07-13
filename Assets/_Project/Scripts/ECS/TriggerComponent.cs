using System;
using System.Collections.Generic;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.Interfaces;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
}

[Serializable]
public class CustomEvent 
{

    public UnityEvent actions;
    public int times = 1;
    public bool noLimit = true;
    private int count;
    private bool initialized;
    

    public void Call()
    {
        if (!initialized)
        {
            initialized = true;
            count = times;
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