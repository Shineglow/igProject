using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] 
        private PlayerInput playerInput;

        private Character character;
        
        private void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            playerInput.actions["HorizontalMain"].performed += OnHorizontalMainChanged;
            playerInput.actions["Jump"].performed += OnJumpPressed;
            playerInput.actions["Interaction"].performed += OnInteractionPressed;
            character = GetComponent<Character>();
        }

        private void OnInteractionPressed(InputAction.CallbackContext obj)
        {
            Debug.Log($"Action pressed.");
            character.Action();
        }

        private void OnJumpPressed(InputAction.CallbackContext obj)
        {
            Debug.Log($"Jump pressed.");
            character.Jump();
        }

        private void OnHorizontalMainChanged(InputAction.CallbackContext obj)
        {
            var readValue = obj.ReadValue<float>();
            Debug.Log($"Horizontal axis changed. Current value: {readValue}");
            character.Move(new Vector2(readValue,0));
        }
    }
}