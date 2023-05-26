using System;
using UnityEngine;

namespace Inputs
{
    public interface IPlayerInput
    {
        /// <summary>
        /// A single tap event
        /// </summary>
        event Action onAttack;
        
        /// <summary>
        /// A swipe event
        /// </summary>
        event Action<Vector2> onDirectionChange;
    }
}