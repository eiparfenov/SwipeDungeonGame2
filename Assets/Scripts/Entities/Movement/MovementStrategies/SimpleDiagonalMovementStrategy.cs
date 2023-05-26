using System;
using Entities.Components;
using Entities.Stats;
using UnityEngine;
using Utils.Extensions;
using Zenject;

namespace Entities.Movement.MovementStrategies
{
    public class SimpleDiagonalMovementStrategy: IMovementStrategy, IInitializable, IDisposable
    {
        private readonly EntityStats _stats;
        private readonly HitInput _hitInput;
        
        private Vector2 _direction;
        
        [Inject]
        public SimpleDiagonalMovementStrategy(EntityStats stats, HitInput hitInput)
        {
            _stats = stats;
            _hitInput = hitInput;
        }

        public int Order => 0;
        public void GetVelocity(ref Vector3 velocity)
        {
            velocity = _direction * _stats.Speed.Value;
        }

        public void Initialize()
        {
            _hitInput.onHit += HitInputOnHit;
        }

        private void HitInputOnHit(Vector2 obj)
        {
            _direction = (_direction + obj).SelectDiagonalAxis();
        }

        public void Dispose()
        {
            _hitInput.onHit -= HitInputOnHit;
        }
    }
}