using System.Collections.Generic;

namespace Maze.Generation
{
    public abstract class MazeCellsGeneratorBase
    {
        protected readonly MazeTheme MazeTheme;

        public MazeCellsGeneratorBase(MazeTheme mazeTheme)
        {
            MazeTheme = mazeTheme;
        }

        public abstract List<MazeCell> GenerateCells();
    }
}