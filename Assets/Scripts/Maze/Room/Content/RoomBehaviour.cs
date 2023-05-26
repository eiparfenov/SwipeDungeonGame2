using System;
using UnityEngine;

namespace Maze.Room.Content
{
    public abstract class RoomBehaviour: MonoBehaviour
    {
        [field: SerializeField] public Guid Id { get; set; }
    }
}