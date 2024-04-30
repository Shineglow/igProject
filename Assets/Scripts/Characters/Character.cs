using CharacterInteractions;
using Characters.Player;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(PlayerPhysicsBody))]
    [RequireComponent(typeof(CharacterShapeAnimatedBody))]
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField]
        private CharacterStats stats;
        private PlayerPhysicsBody playerPhysicsBody;
        private CharacterShapeAnimatedBody characterShapeAnimatedBody;

        [SerializeField] 
        private InteractorComponent interactableDetecter;

        private InteractablesSelectModule interactablesSelectModule;

        public ICharacterStats CharacterStats => stats;
        public Vector2 Velocity => playerPhysicsBody.CurrentVelocity;
        
        // TODO: pass parameters to animator to switch beetween animations
        public bool IsTouchingGround { get; private set; }
        public bool IsGroundNear { get; private set; }

        private void Awake()
        {
            playerPhysicsBody = GetComponent<PlayerPhysicsBody>();
            playerPhysicsBody.SetCharacterStats(stats);
            characterShapeAnimatedBody = GetComponent<CharacterShapeAnimatedBody>();
            
            InteractablesDetecterInitialization();

            playerPhysicsBody.IsGroundNear.OnValueChanged += OnGroundNearChanged;
            playerPhysicsBody.IsTouchingGround.OnValueChanged += OnTouchingGroundChanged;
        }
        
        private void OnTouchingGroundChanged(bool newValue)
        {
            IsTouchingGround = newValue;
        }

        private void OnGroundNearChanged(bool newValue)
        {
            IsGroundNear = newValue;
        }

        private void InteractablesDetecterInitialization()
        {
            if (interactableDetecter == null)
            {
                UnityEngine.Debug.LogWarning(
                    "InteractorComponent is null! The character will not be able to detect objects to interact with");
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
            StupidAnimationSwitch(direction);

            ChangeLookDirection(direction);

            playerPhysicsBody.SetMovementDirection(direction);
        }

        private void ChangeLookDirection(Vector2 direction)
        {
            if (direction.x == 0) return;

            var newLookDirection = direction.x > 0;
            if (newLookDirection != characterShapeAnimatedBody.IsLookAtRight)
                characterShapeAnimatedBody.LookAtRight(newLookDirection);
        }

        private void StupidAnimationSwitch(Vector2 direction)
        {
            var animationName = direction.magnitude > 0 ? "Run" : "Idle";
            characterShapeAnimatedBody.PlayAnimation(animationName);
        }

        public void Jump()
        {
            playerPhysicsBody.Jump(stats.JumpHeight);
        }

        public void Action()
        {
            var interactable = interactablesSelectModule.Interactable;
            if (interactable == null)
                return;
            var animationName = interactable.Interact();
            UnityEngine.Debug.Log($"Passed animation name: {animationName}");
            // TODO: pass animation name (if it not null) to character shape body. The body must play passed animation
            // TODO: or log the reason why it can't be done
        }
    }
}
