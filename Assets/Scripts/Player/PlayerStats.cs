using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable/Stats", order = 1)]
    public class PlayerStats : ScriptableObject, IPlayerStats
    {
        [field: SerializeField]
        public float MaxSeed {get;set;}
        
        [field: SerializeField]
        public float JumpHeight {get;set;}
        
        [field: SerializeField]
        public float AccelerationMax {get;set;}

        [field: SerializeField]
        public AnimationCurve Acceleration {get;set;}
        [field: SerializeField]
        public AnimationCurve Deceleration {get;set;}
    }
}