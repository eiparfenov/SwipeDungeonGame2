using System;
using Maze.Generation;
using Zenject;

namespace Maze.Rooms
{
    public class RoomsLoader: IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly MazeGenerator _mazeGenerator;
        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}