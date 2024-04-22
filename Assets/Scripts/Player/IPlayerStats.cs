using UnityEngine;

namespace Player
{
    public interface IPlayerStats
    {
        public float MaxSeed { get; }
        public float JumpForce { get; }
        public AnimationCurve Acceleration {get;}
        public AnimationCurve Deceleration {get;}
    }
}