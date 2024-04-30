using UnityEngine;

namespace Characters
{
    public interface ICharacter : IControlable2D
    {
        public ICharacterStats CharacterStats { get; }
        
        public Vector2 Velocity { get; }
        
        public bool IsTouchingGround { get; }
        public bool IsGroundNear { get; }
        public bool IsFallingDown => !IsTouchingGround && Velocity.y < 0 && Mathf.Abs(Velocity.y) > Mathf.Abs(Velocity.x);
        public bool IsMoving => IsTouchingGround && Mathf.Abs(Velocity.x) > 0;
    }
}