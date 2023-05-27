using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.MazeInstallers;
using Maze.Generation;
using Zenject;

namespace Maze.Rooms
{
    public class RoomsLoader: IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly MazeGenerator _mazeGenerator;
        private readonly RoomFactory _roomFactory;

        private List<RoomInfo> _createdRooms;

        public RoomsLoader(SignalBus signalBus, MazeGenerator mazeGenerator, RoomFactory roomFactory)
        {
            _signalBus = signalBus;
            _mazeGenerator = mazeGenerator;
            _roomFactory = roomFactory;
        }

        public void Initialize()
        {
            _createdRooms = _mazeGenerator.GenerateRooms();
            _createdRooms.Select(x => _roomFactory.Create(new(x))).ToList();
        }

        public void Dispose()
        {
            
        }
    }
}