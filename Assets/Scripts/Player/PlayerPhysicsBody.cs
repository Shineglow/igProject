using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerPhysicsBody : MonoBehaviour
    {
        private Rigidbody rb;
        private float maxSpeed;
        private float speed;
        
        private ContactPoint[] contactPoints;
        private ContactFilter2D contactFilter;
        private Vector2 normalPerpendicular;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            contactPoints = new ContactPoint[1];
            contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(LayerMask.GetMask("Floor"));
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            
        }

        private void JumpInUpdate(float deltaTime)
        {
            
        }

        public void Jump(float height)
        {
            
        }

        public void SetMoveDirection(Vector2 direction)
        {
            speed = direction.x;
        }
    }
}