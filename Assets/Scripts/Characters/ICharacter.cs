using UnityEngine;

namespace Characters
{
    public interface ICharacter : IControlable2D
    {
        public ICharacterStats CharacterStats { get; }
        
        public Vector2 CurrentVelocity { get; }
        
        public bool IsTouchingGround { get; }
        public bool IsGroundNear { get; }
        public bool IsFallingDown => !IsTouchingGround && CurrentVelocity.y < 0 && Mathf.Abs(CurrentVelocity.y) > Mathf.Abs(CurrentVelocity.x);
        public bool IsMoving => IsTouchingGround && Mathf.Abs(CurrentVelocity.x) > 0;
    }
}