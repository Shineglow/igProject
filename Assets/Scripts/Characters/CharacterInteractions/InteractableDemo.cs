using System;
using UnityEngine;

namespace CharacterInteractions
{
    public class InteractableDemo : MonoBehaviour, IInteractable
    {
        [SerializeField] 
        private Transform _transform;
        public Transform Transform => _transform;

        // TODO: maybe pull this out in some configuration or bind with scriptable object
        [SerializeField] 
        private string _animationName;

        private void Awake()
        {
            _transform = transform;
        }

        public string Interact()
        {
            return _animationName;
        }
    }
}