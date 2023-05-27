using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Maze.Rooms
{
    public class Room: IDisposable
    {
        private readonly Transform _transform;
        private readonly RoomSettings _settings;
        private readonly RoomInfo _roomInfo;

        public Room(Transform transform, RoomSettings settings, RoomInfo roomInfo)
        {
            _transform = transform;
            _settings = settings;
            _roomInfo = roomInfo;

            _transform.position = _roomInfo.Position * _settings.Size + _settings.Offset;
        }

        public void Dispose()
        {
            Object.Destroy(_transform.gameObject);
        }
    }
}