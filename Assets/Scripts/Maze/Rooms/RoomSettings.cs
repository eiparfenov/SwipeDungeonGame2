using System;
using UnityEngine;

namespace Maze.Rooms
{
    [Serializable]
    public class RoomSettings
    {
        [field: SerializeField] public Vector2 Offset { get; private set; }
        [field: SerializeField] public Vector2 Size { get; private set; }
    }
}