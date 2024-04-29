using System;
using UnityEngine;

namespace CharacterInteractions
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class InteractorComponent : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider2D trigger;

        public event Action<IInteractable> InteractableInArea = (interactableInArea) => { };
        public event Action<IInteractable> InteractableLeaveArea = (interactableLeaveArea) => { };

        private void Awake()
        {
            trigger ??= GetComponent<BoxCollider2D>();
            trigger.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                InteractableInArea(interactable);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                InteractableLeaveArea(interactable);
            }
        }
    }
}