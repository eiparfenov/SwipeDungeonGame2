using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Maze.Rooms
{
    public class Room: IDisposable
    {
        private readonly Transform _transform;

        public Room(Transform transform)
        {
            _transform = transform;
        }

        public void Dispose()
        {
            Object.Destroy(_transform.gameObject);
        }
    }
}