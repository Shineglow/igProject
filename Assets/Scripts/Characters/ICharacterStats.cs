using UnityEngine;

namespace Characters
{
    public interface ICharacterStats
    {
        public float MaxSeed { get; }
        public float AccelerationMax { get; }
        public float JumpHeight { get; }

        public AnimationCurve Acceleration {get;}
        public AnimationCurve Deceleration {get;}
    }
}