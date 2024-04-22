using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerPhysicsBody))]
    [RequireComponent(typeof(CharacterShapeAnimatedBody))]
    [RequireComponent(typeof(PlayerController))]
    public class Character : MonoBehaviour, IControlable2D
    {
        [SerializeField]
        private PlayerStats stats;
        private PlayerPhysicsBody playerPhysicsBody;
        private CharacterShapeAnimatedBody characterShapeAnimatedBody;
        private PlayerController playerController;
        
        public IPlayerStats Stats => stats;

        private void Awake()
        {
            playerPhysicsBody = GetComponent<PlayerPhysicsBody>();
            characterShapeAnimatedBody = GetComponent<CharacterShapeAnimatedBody>();
        }

        public void Move(Vector2 direction)
        {
            playerPhysicsBody.SetMoveDirection(direction*stats.MaxSeed);
        }

        public void Jump()
        {
            playerPhysicsBody.Jump(stats.jumpHeight);
        }

        public void Action()
        {
            throw new System.NotImplementedException();
        }
    }
}
