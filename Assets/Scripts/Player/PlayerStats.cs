using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable/Stats", order = 1)]
    public class PlayerStats : ScriptableObject, IPlayerStats
    {
        public float maxSeed;
        public float MaxSeed => maxSeed;
        
        public float jumpHeight;
        public float JumpForce => jumpHeight;
    }
}