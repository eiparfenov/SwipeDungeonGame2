using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Infrastructure.MazeInstallers;
using Maze.Generation;
using Shared;
using Zenject;

namespace Maze.Rooms
{
    public class MazeMainLoop: IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly MazeGenerator _mazeGenerator;
        private readonly RoomFactory _roomFactory;

        private List<RoomInfo> _createdRooms;
        private Room _currentRoom;

        public MazeMainLoop(SignalBus signalBus, MazeGenerator mazeGenerator, RoomFactory roomFactory)
        {
            _signalBus = signalBus;
            _mazeGenerator = mazeGenerator;
            _roomFactory = roomFactory;
        }

        public void Initialize()
        {
            _createdRooms = _mazeGenerator.GenerateRooms();
            _currentRoom = _roomFactory.Create(new RoomCreationDto(_createdRooms[0]));
            _currentRoom.onRoomExited += RoomOnExit;
        }

        public async void RoomOnExit(Side side)
        {
            _currentRoom.onRoomExited -= RoomOnExit;
            var nextRoomInfo = _createdRooms.FirstOrDefault(room => room.Position == _currentRoom.RoomInfo.Position + side.SideDirectionsInt());
            _signalBus.Fire(new RoomChanged(_currentRoom.RoomInfo.Position, nextRoomInfo.Position));
    
            var nextRoom = _roomFactory.Create(new RoomCreationDto(nextRoomInfo));
            await UniTask.Delay(1000);
            _currentRoom.Dispose();
            _currentRoom = nextRoom;
            _currentRoom.onRoomExited += RoomOnExit;
        }

        public void Dispose()
        {
            
        }
    }
}