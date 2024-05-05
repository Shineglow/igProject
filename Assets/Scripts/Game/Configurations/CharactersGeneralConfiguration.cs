using UnityEngine;

namespace Game.Configurations
{
    [CreateAssetMenu(fileName = "CharactersGeneralConfiguration", menuName = "Scriptable/Configurations", order = 1)]
    public class CharactersGeneralConfiguration : ScriptableObject
    {
        [field: SerializeField]
        public float GroundNearTreshold { get; private set; } = 0.3f;
    }
}
