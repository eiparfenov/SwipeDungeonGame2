using System.Collections.Generic;
using System.Linq;
using Entities.Components;
using Entities.Movement.MovementStrategies;
using Entities.Stats;
using UnityEngine;
using Zenject;

namespace Entities.Movement
{
    public class MovementController: ITickable
    {
        private readonly List<IMovementStrategy> _movementStrategies;
        private readonly MovementOutput _movementOutput;
        private readonly EntityStats _stats;

        [Inject]
        public MovementController(List<IMovementStrategy> movementStrategies, MovementOutput movementOutput,
            EntityStats stats)
        {
            _stats = stats;
            _movementStrategies = movementStrategies.OrderBy(x => x.Order).ToList();
            _movementOutput = movementOutput;
        }


        public void Tick()
        {
            var velocity = new Vector3();
            foreach (var movementStrategy in _movementStrategies)
            {
                movementStrategy.GetVelocity(ref velocity);
            }
            _movementOutput.ApplyVelocity(velocity);
            _stats.Velocity.Value = velocity;
        }
    }
}