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
            character = GetComponent<Character>();
        }

        private void OnJumpPressed(InputAction.CallbackContext obj)
        {
            var readValue = obj.ReadValue<bool>();
            Debug.Log($"Jump changed. Current value: {readValue}");
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