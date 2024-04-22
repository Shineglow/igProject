using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerPhysicsBody : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private float _maxSpeed;
        private Vector2 _movementVector;
        
        private readonly ContactPoint2D[] _contactPoints = new ContactPoint2D[1];
        private ContactFilter2D _contactFilter;
        private Vector2 _normalPerpendicular;
        private Vector3 _jumpForce;
        private bool _hasContactsWithFloor;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _contactFilter = new ContactFilter2D();
            _contactFilter.SetLayerMask(LayerMask.GetMask("Floor"));
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            _hasContactsWithFloor = _contactPoints != null && _rb.GetContacts(_contactFilter, _contactPoints) > 0;

            JumpInUpdate(deltaTime);
        }

        private void JumpInUpdate(float deltaTime)
        {
            if (_hasContactsWithFloor)
            {
                if (_jumpForce.magnitude > 0)
                {
                    _rb.AddForce(_jumpForce, ForceMode2D.Impulse);
                    _jumpForce = Vector3.zero;
                }
            }
        }

        public void Jump(float height)
        {
            var forceUp = _rb.mass * Mathf.Sqrt(2f * -Physics.gravity.y * height);
            _jumpForce = new Vector3(0, forceUp, 0);
            Debug.Log($"_jumpForce = {_jumpForce}");
        }

        public void SetMoveDirection(Vector2 direction)
        {
            _movementVector = direction;
        }
    }
}