using System;
using Extensions;
using JetBrains.Annotations;
using UnityEngine;

namespace Characters
{
    public class AccelerationModule
    {
        private IPlayerStats _playerStats;

        /// <summary>
        /// speed change between -maxSpeed and maxSpeed
        /// </summary>
        private float _speed = 0f;

        private float _horizontalDirection;

        public float GetActualSpeed(Vector2 inputDirection, float deltaTime, [NotNull] IPlayerStats _playerStats)
        {
            this._playerStats = _playerStats;
            _horizontalDirection = GetDirection(inputDirection.x);
            
            var speedDirection = GetDirection(_speed);
            var normalizedSpeed = Mathf.Abs(_speed / _playerStats.MaxSeed);
            
            _speed = CalculateActualSpeed(normalizedSpeed, speedDirection, deltaTime);

            return Mathf.Clamp(Mathf.Abs(_speed), 0f, _playerStats.MaxSeed);
        }

        private float CalculateActualSpeed(float normalizedSpeed, float speedDirection, float deltaTime)
        {
            var result = _speed;
            
            var deceleration = GetCurrentDeceleration(normalizedSpeed) * deltaTime;

            if (ApplyAccelerationIfDirectionIsZero(speedDirection, deceleration, ref result)) 
                return result;

            if (ApplyAccelerationIfStartMovingOrMoveInSameDirections(normalizedSpeed, speedDirection, deltaTime, ref result)) 
                return result;

            result -= deceleration * speedDirection;
            return result;
        }

        private bool ApplyAccelerationIfStartMovingOrMoveInSameDirections(float normalizedSpeed, float speedDirection, float deltaTime, ref float actualSpeed)
        {
            if (!speedDirection.IsEqualZero() && !(Math.Abs(speedDirection - _horizontalDirection) < 0.001f))
                return false;
            
            var actualAcceleration = GetCurrentAcceleration(normalizedSpeed) * deltaTime;
            actualSpeed += actualAcceleration * _horizontalDirection;
            return true;
        }

        private bool ApplyAccelerationIfDirectionIsZero(float speedDirection, float deceleration, ref float actualSpeed)
        {
            if (!_horizontalDirection.IsEqualZero()) 
                return false;
            
            if (_speed.IsEqualZero())
                return true;

            actualSpeed -= deceleration * speedDirection;

            if (Math.Abs(speedDirection - GetDirection(_speed)) < 0.001f)
            {
                actualSpeed = 0f;
            }

            return true;
        }
        
        private float GetCurrentDeceleration(float normalizedSpeed)
        {
            return _playerStats.AccelerationMax * _playerStats.Deceleration.Evaluate(normalizedSpeed);
        }

        private float GetCurrentAcceleration(float normalizedSpeed)
        {
            return _playerStats.AccelerationMax * _playerStats.Acceleration.Evaluate(normalizedSpeed);
        }

        private float GetDirection(float f) => f.IsEqualZero() ? 0f : Mathf.Sign(f);
    }
}