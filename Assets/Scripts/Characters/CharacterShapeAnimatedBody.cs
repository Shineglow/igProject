using UnityEngine;

namespace Characters
{
    public class CharacterShapeAnimatedBody : MonoBehaviour, IAnimatedObject
    {
        [SerializeField]
        private Animator animator;

        public void PlayAnimation(string animationName)
        {
            animator.Play(animationName);
        }
    }
}