using System;
using Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters
{
    [Serializable]
    public class AccelerationModule
    {
        private ICharacterStats _characterStats;

        /// <summary>
        /// speed change between -maxSpeed and maxSpeed
        /// </summary>
        [SerializeField]
        private float speed = 0f;

        private float _horizontalDirection;

        public float GetActualSpeed(Vector2 inputDirection, float deltaTime, [NotNull] ICharacterStats characterStats)
        {
            speed = GetActualSpeedSigned(inputDirection, deltaTime, characterStats);

            return Mathf.Abs(speed);
        }
        
        public float GetActualSpeedSigned(Vector2 inputDirection, float deltaTime, [NotNull] ICharacterStats characterStats)
        {
            _characterStats = characterStats;
            _horizontalDirection = GetDirection(inputDirection.x);

            speed = CalculateActualSpeed(inputDirection, deltaTime, characterStats);
            
            speed = Mathf.Clamp(speed, -_characterStats.MaxSeed, _characterStats.MaxSeed);

            return speed;
        }

        public float GetCurrentAcceleration(Vector2 inputDirection, float deltaTime,
            [NotNull] ICharacterStats characterStats)
        {
            _characterStats = characterStats;
            _horizontalDirection = GetDirection(inputDirection.x);
            
            var speedDirection = GetDirection(speed);
            var normalizedSpeed = Mathf.Abs(speed) / _characterStats.MaxSeed;

            var isSpeedDirectionSameAsInput = Math.Abs(speedDirection - _horizontalDirection) < 0.001f;
            var isAccelerateNow = isSpeedDirectionSameAsInput || speedDirection == 0;
            var actualAcceleration = isAccelerateNow
                ? GetCurrentAcceleration(normalizedSpeed) * _horizontalDirection
                : GetCurrentDeceleration(normalizedSpeed) * -speedDirection;

            return actualAcceleration * deltaTime;
        }

        private float CalculateActualSpeed(Vector2 inputDirection, float deltaTime,
            [NotNull] ICharacterStats characterStats)
        {
            var result = speed;
            
            var isInputDirectionZero = _horizontalDirection == 0f;
            
            var speedDirection = GetDirection(speed);

            result += GetCurrentAcceleration(inputDirection, deltaTime, characterStats);

            if (Math.Abs(speedDirection - GetDirection(result)) > 0.0001f && isInputDirectionZero)
                result = 0;
            
            return result;
        }
        
        private float GetCurrentDeceleration(float normalizedSpeed)
        {
            return _characterStats.AccelerationMax * _characterStats.Deceleration.Evaluate(normalizedSpeed);
        }

        private float GetCurrentAcceleration(float normalizedSpeed)
        {
            return _characterStats.AccelerationMax * _characterStats.Acceleration.Evaluate(normalizedSpeed);
        }

        private float GetDirection(float f) => f == 0f ? 0f : Mathf.Sign(f);
    }
}