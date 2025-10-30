using System;
using UnityEngine;

namespace _Project.Scripts.Player {
    public class SimpleTeleportTrigger : MonoBehaviour {
        [SerializeField] SimpleTeleportTrigger exitTrigger;

        public bool isInside = false;
        
        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player") && !isInside) {
                exitTrigger.isInside = true;
                other.transform.position = exitTrigger.transform.position;
            }
        }

        private void OnTriggerExit(Collider other) {
            isInside = false;
        }
    }
}