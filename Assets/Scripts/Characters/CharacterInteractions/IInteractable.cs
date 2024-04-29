using UnityEngine;

namespace CharacterInteractions
{
    public interface IInteractable
    {
        public Transform Transform { get; }
        /// <summary>
        /// Do action with some object.
        /// </summary>
        /// <returns>Animation name.</returns>
        public string Interact();
    }
}