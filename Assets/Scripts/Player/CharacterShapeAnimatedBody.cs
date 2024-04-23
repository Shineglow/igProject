using System;
using UnityEngine;

namespace Player
{
    public interface IAnimatedObject
    {
        void PlayAnimation(string animationName);
    }

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