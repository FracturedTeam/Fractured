using UnityEngine;

namespace _Project.Scripts.ECS {
    public class SceneMaster : MonoBehaviour {
        
        //Soit chacun à un sceneElement avec un state validated dans lequel on peut choisir quel est l'état rempli pour être validated
        //Lorsqu'il est validated => il envoie l'info au MemoryScene
        //Lorsque tout les objets du memory scene sont validated => Trigger son event (souvenir / shard / objet)
        
        
        //SceneElement qui est sur l'objet

        private void SetValidationToSceneElement() {
            
        }

        public void CheckForValidation() {
            
        }
    }
}