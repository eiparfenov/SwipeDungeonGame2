using UnityEngine;

namespace Entities.Movement.MovementStrategies
{
    public interface IMovementStrategy
    {
        /// <summary>
        /// Order of strategies usage
        /// </summary>
        int Order { get; }
        
        /// <summary>
        /// Changes velocity using entity stats
        /// </summary>
        /// <param name="velocity">the velocity to change</param>
        void GetVelocity(ref Vector3 velocity);
    }
}