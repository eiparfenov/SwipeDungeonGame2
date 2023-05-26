using System;
using UnityEngine;

namespace Maze.Rooms
{
    [Serializable]
    public class RoomInfo: MazeCell
    {
        [field: SerializeField] public int[] SelectedWall { get; init; }
        [field: SerializeField] public int[] SelectedGate { get; init; }
        [field: SerializeField] public RoomContentInfo[] RoomContents { get; init; }

        public RoomInfo(MazeCell baseCell)
        {
            Position = baseCell.Position;
            Gates = baseCell.Gates;
            Depth = baseCell.Depth;
        }
    }

    [Serializable]
    public class RoomContentInfo
    {
        [field: SerializeField] public Guid RoomBehaviour { get; init; }
        [field: SerializeField] public Vector2 Position { get; init; }
    }
}