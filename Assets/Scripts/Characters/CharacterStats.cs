using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable/Stats", order = 1)]
    public class CharacterStats : ScriptableObject, ICharacterStats
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