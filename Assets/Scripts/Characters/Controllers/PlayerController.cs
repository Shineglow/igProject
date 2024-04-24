using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Characters.Controllers
{
    public class PlayerController : ICharacterController
    {
        private PlayerInput playerInput;
        private IControlable2D character;

        public bool IsControllableNull => character == null;
        public bool IsInputNull => playerInput == null;

        private Action UnbindAllInputActions = delegate { };

        [Inject]
        public void Construct(IControlable2D character, PlayerInput input)
        {
            SetCharacter(character);
            SetInput(input);
        }
        
        public void SetCharacter(IControlable2D character)
        {
            this.character = character;
        }

        private void ClearInputSubscriptions()
        {
            UnbindAllInputActions();
            UnbindAllInputActions = delegate { };
        }

        public void SetInput(PlayerInput input)
        {
            ClearInputSubscriptions();
            playerInput = input;
            if(playerInput != null)
                SubscribeOnInputActions();
        }

        private void SubscribeOnInputActions()
        {
            SmartSubscribeOnAction("HorizontalMain", OnHorizontalMainChanged);
            SmartSubscribeOnAction("Jump", OnJumpPressed);
            SmartSubscribeOnAction("Interaction", OnInteractionPressed);
        }

        private void SmartSubscribeOnAction(string actionName,
            Action<InputAction.CallbackContext> callbackProcessMethod)
        {
            if (playerInput == null)
            {
                Debug.LogError("Trying to subscribe on not set input!");
                return;
            }
            
            playerInput.actions[actionName].performed += callbackProcessMethod;
        }

        private void OnInteractionPressed(InputAction.CallbackContext obj)
        {
            Debug.Log($"Action pressed.");
            character?.Action();
        }

        private void OnJumpPressed(InputAction.CallbackContext obj)
        {
            Debug.Log($"Jump pressed.");
            character?.Jump();
        }

        private void OnHorizontalMainChanged(InputAction.CallbackContext obj)
        {
            var readValue = obj.ReadValue<float>();
            Debug.Log($"Horizontal axis changed. Current value: {readValue}");
            character?.Move(new Vector2(readValue,0));
        }
    }
}