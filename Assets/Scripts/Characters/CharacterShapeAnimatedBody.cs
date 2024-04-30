using System;
using UnityEngine;

namespace Characters
{
    public class CharacterShapeAnimatedBody : MonoBehaviour, IAnimatedObject
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Animator animator;
        
        public bool IsLookAtRight => !spriteRenderer.flipX;

        public void PlayAnimation(string animationName)
        {
            animator.Play(animationName);
        }
        
        public void LookAtRight(bool isLookAtRight)
        {
            // default orientation is to the right
            // if we need to look at right, we must invert input parameter
            spriteRenderer.flipX = !isLookAtRight;
        }

        public void SetFlag(string flagName, bool value)
        {
            animator.SetBool(flagName, value);
        }
    }
}