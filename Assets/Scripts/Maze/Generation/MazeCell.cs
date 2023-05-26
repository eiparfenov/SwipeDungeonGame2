using System;
using System.Collections.Generic;
using Shared;
using UnityEngine;

namespace Maze.Generation
{
    [Serializable]
    public class MazeCell
    {
        [field: SerializeField] public Vector2Int Position { get; init; }
        [field: SerializeField] public List<Side> Gates { get; private set; } 
        [field: SerializeField] public int Depth { get; init; }
        #region RelativePositioning

        public bool IsCorner => Gates.Count == 1;
        public bool IsPath => Gates.Count == 2 && Gates[0].Opposite() == Gates[1];
        public bool IsTurn => Gates.Count == 2 && Gates[0].Opposite() == Gates[1];
        public bool IsTCross => Gates.Count == 3;
        public bool IsCross => Gates.Count == 4;
        
        #endregion

        public MazeCell()
        {
            Gates = new List<Side>();
        }
    }
}