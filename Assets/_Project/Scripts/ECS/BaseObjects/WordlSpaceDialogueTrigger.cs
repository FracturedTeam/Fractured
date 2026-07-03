using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.Systems.Timers;
using UnityEngine;

public class WordlSpaceDialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<WorldSpaceDialogue> dialogues = new List<WorldSpaceDialogue>(); 
    
    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Player")) 
            return;

        
        foreach (var dialogue in dialogues)

            StartCoroutine(Appearing(dialogue));
        
    }
    

    [ContextMenu("Reload")]
    private IEnumerator Appearing(WorldSpaceDialogue dialogue)
    {
        yield return new WaitForSeconds(dialogue.delay);
        GlassTextManager.Instance.SetUpWorldSpaceText(dialogue.textTransform, dialogue.glassTextScriptableObject);
    }
}

[Serializable]
public struct WorldSpaceDialogue
{
    public GlassTextScriptableObject glassTextScriptableObject;
    public float delay;
    public Transform textTransform;
}
