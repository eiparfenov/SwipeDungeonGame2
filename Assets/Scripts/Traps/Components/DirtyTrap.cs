using Maze.Components;
using Maze.Rooms;
using Traps.Interfaces;
using UnityEngine;

namespace Traps.Components
{
    public class DirtyTrap: RoomInteractiveBehaviour
    {
        [Tooltip("Multiplies entities speed by this value, when entity is on this trap")]
        [SerializeField] private float speedK;
        private void OnTriggerEnter2D(Collider2D col)
        {
            var dirtyTrapInput = col.GetComponent<IDirtyTrapHandler>();
            if(dirtyTrapInput == null) return;
            dirtyTrapInput.OnTrapEnter(speedK);
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            var dirtyTrapInput = col.GetComponent<IDirtyTrapHandler>();
            if(dirtyTrapInput == null) return;
            dirtyTrapInput.OnTrapExit(speedK);
        }
    }
}