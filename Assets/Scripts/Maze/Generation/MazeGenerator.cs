using System.Collections.Generic;
using Maze.Rooms;

namespace Maze.Generation
{
    public class MazeGenerator
    {
        private readonly MazeInfoGeneratorBase _mazeInfoGenerator;
        private readonly MazeCellsGeneratorBase _mazeCellsGenerator;

        public MazeGenerator(MazeInfoGeneratorBase mazeInfoGenerator, MazeCellsGeneratorBase mazeCellsGenerator)
        {
            _mazeInfoGenerator = mazeInfoGenerator;
            _mazeCellsGenerator = mazeCellsGenerator;
        }


        public List<RoomInfo> GenerateRooms()
        {
            var cells = _mazeCellsGenerator.GenerateCells();
            return _mazeInfoGenerator.GenerateRooms(cells);
        }
    }
}