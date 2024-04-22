using System;
using System.Collections.Generic;
using CharacterInteractions;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerPhysicsBody))]
    [RequireComponent(typeof(CharacterShapeAnimatedBody))]
    [RequireComponent(typeof(PlayerController))]
    public class Character : MonoBehaviour, IControlable2D
    {
        [SerializeField]
        private PlayerStats stats;
        private PlayerPhysicsBody playerPhysicsBody;
        private CharacterShapeAnimatedBody characterShapeAnimatedBody;
        private PlayerController playerController;

        [SerializeField] 
        private InteractorComponent interactableDetecter;

        private InteractablesSelectModule interactablesSelectModule;
        
        public IPlayerStats Stats => stats;

        private void Awake()
        {
            playerPhysicsBody = GetComponent<PlayerPhysicsBody>();
            characterShapeAnimatedBody = GetComponent<CharacterShapeAnimatedBody>();
            if (interactableDetecter == null)
            {
                Debug.LogWarning("InteractorComponent is null! The character will not be able to detect objects to interact with");
            }
            else
            {
                interactablesSelectModule = new InteractablesSelectModule();
                interactableDetecter.InteractableInArea += interactablesSelectModule.OnInteractableEnterArea;
                interactableDetecter.InteractableLeaveArea += interactablesSelectModule.OnInteractableLeaveArea;
            }
        }

        public void Move(Vector2 direction)
        {
            playerPhysicsBody.SetMoveDirection(direction*stats.MaxSeed);
        }

        public void Jump()
        {
            playerPhysicsBody.Jump(stats.jumpHeight);
        }

        public void Action()
        {
            var interactable = interactablesSelectModule.Interactable;
            if (interactable == null)
                return;
            var animationName = interactable.Interact();
            Debug.Log($"Passed animation name: {animationName}");
            // TODO: pass animation name (if it not null) to character shape body. The body must play passed animation
            // TODO: or log the reason why it can't be done
        }
    }
}
