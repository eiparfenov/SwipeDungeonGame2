using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Maze.Components;
using Shared;
using UnityEngine;
using Utils.Extensions;
using Zenject;
using Object = UnityEngine.Object;

namespace Maze.Rooms
{
    public class Room: IDisposable, IInitializable
    {
        private readonly Transform _transform;
        private readonly RoomSettings _settings;
        private readonly RoomInfo _roomInfo;
        private readonly CancellationToken _cancellationToken;
        private readonly List<RoomSide> _roomSides;

        public event Action<Side> onRoomExited;

        public RoomInfo RoomInfo => _roomInfo;

        public Room(
            Transform transform, 
            RoomSettings settings, 
            RoomInfo roomInfo, 
            [InjectLocal] CancellationToken cancellationToken,
            List<RoomSide> roomSides)
        {
            _transform = transform;
            _settings = settings;
            _roomInfo = roomInfo;
            _cancellationToken = cancellationToken;
            _roomSides = roomSides;
            
            _transform.position = _roomInfo.Position * _settings.Size + _settings.Offset;
        }

        public void Initialize()
        {
            _roomSides.Foreach(side => side.onRoomExit += SideOnRoomExit);
            MainLoop(_cancellationToken);
        }

        private void SideOnRoomExit(Side side)
        {
            onRoomExited?.Invoke(side);
        }

        private async void MainLoop(CancellationToken cancellationToken)
        {
            _roomSides
                .Where(roomSide => _roomInfo.Gates.Contains(roomSide.Side))
                .Foreach(roomSide => roomSide.SetDoorState(true));
        }

        public void Dispose()
        {
            _roomSides.Foreach(side => side.onRoomExit -= SideOnRoomExit);
            Object.Destroy(_transform.gameObject);
        }
    }
}