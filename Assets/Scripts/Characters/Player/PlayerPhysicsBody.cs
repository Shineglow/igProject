using JetBrains.Annotations;
using UnityEngine;
using Utils.SmartTypes;
using Zenject;

namespace Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerPhysicsBody : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField]
        private Transform raycastPoint;
        [SerializeField]
        private AccelerationModule accelerationModule;

        #region PhysicsData

        private RaycastHit2D[] hitInfos;
        private readonly ContactPoint2D[] _contactPoints = new ContactPoint2D[1];
        private ContactFilter2D _groundFilter;
        
        private Vector2 _normalPerpendicular;
        public Vector2 VelocityVector => _rb.velocity;

        #endregion

        #region ObservableProperties

        [SerializeField]
        private ObservableProperty<bool> _isTouchingGround = new(false);
        public IObservableProperty<bool> IsTouchingGround => _isTouchingGround;

        [SerializeField]
        private ObservableProperty<bool> _isGroundNear = new(false);
        public IObservableProperty<bool> IsGroundNear => _isGroundNear;

        [SerializeField]
        private ObservableProperty<Vector2> _currentVelocity = new (Vector2.zero);
        public IObservableProperty<Vector2> CurrentVelocity => _currentVelocity;

        #endregion

        #region ControllerSetableParameters

        private Vector2 _movementVector;
        private Vector2 _jumpForce;

        #endregion

        private ICharacterStats _stats;
        
        private static float GROUND_NEAR_TRESHOLD = 0.3f;

        [Inject]
        public void Construct(AccelerationModule accelerationModule)
        {
            this.accelerationModule = accelerationModule;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _groundFilter = new ContactFilter2D();
            _groundFilter.SetLayerMask(LayerMask.GetMask("Floor"));
            hitInfos = new RaycastHit2D[1];
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            _isTouchingGround.Value = _contactPoints != null && _rb.GetContacts(_groundFilter, _contactPoints) > 0;

            UpdateGroundNearValue();

            JumpInUpdate(deltaTime);
            
            if(_isTouchingGround.Value)
                MoveUnderGround(deltaTime);
            
            UpdateCurrentVelocity();
        }

        private void UpdateCurrentVelocity()
        {
            _currentVelocity.Value = _rb.velocity;
        }

        private void UpdateGroundNearValue()
        {
            if (_isTouchingGround.Value)
            {
                _isGroundNear.Value = true;
                return;
            }

            if (Physics2D.Raycast(raycastPoint.position, Vector2.down, _groundFilter, hitInfos) <= 0)
            {
                _isGroundNear.Value = false;
                return;
            }
            
            var hitInfo = hitInfos[0];
            _isGroundNear.Value = hitInfo.distance < GROUND_NEAR_TRESHOLD;
        }

        private void MoveUnderGround(float deltaTime)
        {
            if (!_isTouchingGround.Value) 
                return;
            
            var actualSpeed = accelerationModule.GetActualSpeedSigned(_movementVector, deltaTime, _stats);
            
            var surfaceNormal = _contactPoints[0].normal;
            var normalOrthogonal = new Vector2(surfaceNormal.y, -surfaceNormal.x);
            var relativeMovement = normalOrthogonal * actualSpeed;

            _rb.velocity = relativeMovement;
        }

        private void JumpInUpdate(float deltaTime)
        {
            if (!_isTouchingGround.Value || !(_jumpForce.magnitude > 0)) return;
            
            _rb.AddForce(_jumpForce, ForceMode2D.Impulse);
            _jumpForce = Vector3.zero;
            _isTouchingGround.Value = false;
        }

        public void Jump(float height)
        {
            var forceUp = _rb.mass * Mathf.Sqrt(2f * -Physics.gravity.y * height);
            _jumpForce = Vector3.up * forceUp;
            UnityEngine.Debug.Log($"_jumpForce = {_jumpForce}");
        }

        public void SetMovementDirection(Vector2 direction)
        {
            _movementVector = direction;
        }

        public void SetCharacterStats([NotNull] ICharacterStats stats)
        {
            _stats = stats;
        }
    }
}