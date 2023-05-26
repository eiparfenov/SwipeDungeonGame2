using System;
using UnityEngine;

namespace Maze.Generation
{
    [Serializable]
    public class MazeGenerationSettings
    {
        [field: SerializeField] public int RoomsCount { get; private set; }
    }
}