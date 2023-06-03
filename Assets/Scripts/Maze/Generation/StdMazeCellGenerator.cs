using System.Collections.Generic;
using System.Linq;
using Shared;
using UnityEngine;
using Utils.Extensions;

namespace Maze.Generation
{
    public class StdMazeCellGenerator: MazeCellsGeneratorBase
    {
        public StdMazeCellGenerator(MazeTheme mazeTheme) : base(mazeTheme)
        {
        }
        
        public override List<MazeCell> GenerateCells()
        {
            var created = new List<MazeCell>();
            var possibleToCreate = new List<GenerationMazeCell>();
            
            var startCell = new GenerationMazeCell() { Depth = 0, Position = Vector2Int.zero, Parent = null };
            created.Add(startCell);
            AddPossibleCells(startCell, possibleToCreate, created);

            for (int cell = 0; cell < MazeTheme.RoomsCount - 1; cell++)
            {
                var cellToCreate = possibleToCreate.RandomOrDefaultWithWeight(cell => 1f / cell.Depth);
                possibleToCreate.Remove(cellToCreate);
                created.Add(cellToCreate);
                
                cellToCreate.Gates.Add(cellToCreate.SideToAdd.Opposite());
                cellToCreate.Parent?.Gates.Add(cellToCreate.SideToAdd);
                
                AddPossibleCells(cellToCreate, possibleToCreate, created);
            }
                
            return created;
        }
        private void AddPossibleCells(GenerationMazeCell addedCell, List<GenerationMazeCell> possibleToCreate, List<MazeCell> created)
        {
            foreach (var side in SideExtensions.AllSides)
            {
                var newPosition = addedCell.Position + side.SideDirectionsInt();
                if (possibleToCreate.Any(cell => cell.Position == newPosition) || created.Any(cell => cell.Position == newPosition)) continue;
                possibleToCreate.Add(new GenerationMazeCell() { Depth = addedCell.Depth + 1, Position = newPosition, SideToAdd = side, Parent = addedCell});
            }
        }
        private class GenerationMazeCell : MazeCell
        {
            public MazeCell Parent { get; init; }
            public Side SideToAdd { get; init; }
        }
    }
}