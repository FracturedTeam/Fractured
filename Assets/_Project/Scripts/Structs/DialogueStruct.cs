using System;
using _Project.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Structs
{
    [Serializable]
    public struct Dialogue
    {
        public DialogueScriptableObject dialogue;
        public bool oneTime;
        [HideInInspector] public bool alreadyInteracted;
    }
}