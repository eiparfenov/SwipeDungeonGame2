using System.Collections.Generic;
using Maze.Rooms;

namespace Maze.Generation
{
    public abstract class MazeInfoGeneratorBase
    {
        protected readonly MazeTheme MazeTheme;

        public MazeInfoGeneratorBase(MazeTheme mazeTheme)
        {
            MazeTheme = mazeTheme;
        }

        public abstract List<RoomInfo> GenerateRooms(List<MazeCell> mazeCells);
    }
}