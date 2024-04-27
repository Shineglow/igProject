using CharacterInteractions;
using Characters.Player;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(PlayerPhysicsBody))]
    [RequireComponent(typeof(CharacterShapeAnimatedBody))]
    public class Character : MonoBehaviour, IControlable2D
    {
        [SerializeField]
        private PlayerStats stats;
        private PlayerPhysicsBody playerPhysicsBody;
        private CharacterShapeAnimatedBody characterShapeAnimatedBody;

        [SerializeField] 
        private InteractorComponent interactableDetecter;

        private InteractablesSelectModule interactablesSelectModule;
        private AccelerationModule _accelerationModule;
        
        public IPlayerStats Stats => stats;

        private void Awake()
        {
            playerPhysicsBody = GetComponent<PlayerPhysicsBody>();
            characterShapeAnimatedBody = GetComponent<CharacterShapeAnimatedBody>();
            
            InteractablesDetecterInitialization();
            
            _accelerationModule = new AccelerationModule();
        }

        private void InteractablesDetecterInitialization()
        {
            if (interactableDetecter == null)
            {
                Debug.LogWarning(
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

            CalculateSpeed(direction);
        }

        private void CalculateSpeed(Vector2 direction)
        {
            var actualSpeed = _accelerationModule.GetActualSpeed(direction, Time.deltaTime, stats);

            playerPhysicsBody.SetSpeedVector(direction * actualSpeed);
        }

        private void ChangeLookDirection(Vector2 direction)
        {
            if (direction.x != 0)
            {
                var newLookDirection = direction.x > 0;
                if (newLookDirection != characterShapeAnimatedBody.IsLookAtRight)
                    characterShapeAnimatedBody.LookAtRight(newLookDirection);
            }
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
            Debug.Log($"Passed animation name: {animationName}");
            // TODO: pass animation name (if it not null) to character shape body. The body must play passed animation
            // TODO: or log the reason why it can't be done
        }
    }
}
