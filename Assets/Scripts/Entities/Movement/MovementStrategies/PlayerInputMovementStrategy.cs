using System;
using Entities.Components;
using Entities.Stats;
using Inputs;
using UnityEngine;
using Zenject;

namespace Entities.Movement.MovementStrategies
{
    public class PlayerInputMovementStrategy: IMovementStrategy, IInitializable, IDisposable
    {
        private Vector3 _direction;
        private readonly EntityStats _stats;
        private readonly IPlayerInput _playerInput;
        private readonly HitInput _hitInput;
        
        [Inject]
        public PlayerInputMovementStrategy(EntityStats stats, IPlayerInput playerInput, HitInput hitInput)
        {
            _stats = stats;
            _playerInput = playerInput;
            _hitInput = hitInput;
        }

        public int Order => 0;
        
        public void GetVelocity(ref Vector3 velocity)
        {
            velocity = _direction.normalized * _stats.Speed.Value;
        }

        public void Initialize()
        {
            _playerInput.onDirectionChange += PlayerInputOnDirectionChange;
            _hitInput.onHit += HitInputOnHit;
        }

        private void HitInputOnHit(Vector2 obj)
        {
            _direction = -_direction;
        }

        private void PlayerInputOnDirectionChange(Vector2 newDirection)
        {
            _direction = newDirection;
        }

        public void Dispose()
        {
            _playerInput.onDirectionChange -= PlayerInputOnDirectionChange;
            _hitInput.onHit -= HitInputOnHit;
        }
    }
}