using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.MazeInstallers;
using Maze.Generation;
using Maze.Rooms;
using Shared;
using Utils.Extensions;
using Zenject;

namespace Maze
{
    public class DebugMazeMainLoop: IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly MazeGenerator _mazeGenerator;
        private readonly RoomFactory _roomFactory;
        private readonly GlobalSettings _globalSettings;

        private List<Room> _createdRooms;
        private Room _currentRoom;

        public DebugMazeMainLoop(SignalBus signalBus, MazeGenerator mazeGenerator, RoomFactory roomFactory, GlobalSettings globalSettings)
        {
            _signalBus = signalBus;
            _mazeGenerator = mazeGenerator;
            _roomFactory = roomFactory;
            _globalSettings = globalSettings;
        }

        public void Initialize()
        {
            _createdRooms = _mazeGenerator.GenerateRooms()
                .Select(info => _roomFactory.Create(new(info)))
                .ToList();
            _currentRoom = _createdRooms[0];
            _currentRoom.onRoomExited += RoomOnExit;
        }

        public async void RoomOnExit(Side side)
        {
            _currentRoom.onRoomExited -= RoomOnExit;
            var nextRoom = _createdRooms.FirstOrDefault(room => room.RoomInfo.Position == _currentRoom.RoomInfo.Position + side.SideDirectionsInt());
            if (nextRoom == null) return;
            _signalBus.Fire(new RoomChanged(_currentRoom.RoomInfo.Position, nextRoom.RoomInfo.Position));
    
            await UniTaskExt.Delay(_globalSettings.RoomExitTime);
            _currentRoom = nextRoom;
            _currentRoom.onRoomExited += RoomOnExit;
        }

        public void Dispose()
        {
            _currentRoom.onRoomExited -= RoomOnExit;
        }
    }
}