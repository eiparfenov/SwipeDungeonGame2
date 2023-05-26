using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shared
{
    public static class SideExtensions
    {
        public static Vector2 SideDirections(this Side side)
        {
            return side switch
            {
                Side.Top => Vector2.up,
                Side.Right => Vector2.right,
                Side.Bottom => Vector2.down,
                Side.Left => Vector2.left,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
        }

        public static Side Opposite(this Side side)
        {
            return side switch
            {
                Side.Top => Side.Bottom,
                Side.Right => Side.Left,
                Side.Bottom => Side.Top,
                Side.Left => Side.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
        }

        public static Vector2Int SideDirectionsInt(this Side side)
        {
            return side switch
            {
                Side.Top => Vector2Int.up,
                Side.Right => Vector2Int.right,
                Side.Bottom => Vector2Int.down,
                Side.Left => Vector2Int.left,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
        }

        public static IEnumerable<Side> AllSides => new[] { Side.Top, Side.Right, Side.Bottom, Side.Left };
    }
}