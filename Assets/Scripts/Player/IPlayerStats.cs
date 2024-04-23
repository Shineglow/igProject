using UnityEngine;

namespace Player
{
    public interface IPlayerStats
    {
        public float MaxSeed { get; }
        public float JumpHeight { get; }
        public AnimationCurve Acceleration {get;}
        public AnimationCurve Deceleration {get;}
    }
}