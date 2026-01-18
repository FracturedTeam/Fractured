using System;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private TutorialElement startElement;
    private bool triggered;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
            startElement.TriggerEventStart();
    }
}
