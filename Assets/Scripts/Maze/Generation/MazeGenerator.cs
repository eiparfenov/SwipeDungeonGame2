using System.Collections.Generic;
using System.Linq;
using Maze.Rooms;
using Shared;
using UnityEngine;
using Utils.Extensions;

namespace Maze.Generation
{
    public class MazeGenerator
    {
        private readonly MazeGenerationSettings _settings;
        public MazeGenerator(MazeGenerationSettings settings)
        {
            _settings = settings;
        }
        private List<MazeCell> GenerateCells()
        {
            var created = new List<MazeCell>();
            var possibleToCreate = new List<GenerationMazeCell>();
            
            var startCell = new GenerationMazeCell() { Depth = 0, Position = Vector2Int.zero, Parent = null };
            created.Add(startCell);
            AddPossibleCells(startCell, possibleToCreate);

            for (int cell = 0; cell < _settings.RoomsCount - 1; cell++)
            {
                var cellToCreate = possibleToCreate.RandomOrDefaultWithWeight(cell => 1f / cell.Depth);
                possibleToCreate.Remove(cellToCreate);
                created.Add(cellToCreate);
                
                cellToCreate.Gates.Add(cellToCreate.SideToAdd.Opposite());
                cellToCreate.Parent.Gates.Add(cellToCreate.SideToAdd);
                
                AddPossibleCells(cellToCreate, possibleToCreate);
            }

            return created;
        }

        public List<RoomInfo> GenerateRooms()
        {
            var rooms = GenerateCells().Select(cell => new RoomInfo(cell)
            {
                SelectedGate = new int[]
                {
                    Random.Range(0, 5),
                    Random.Range(0, 5),
                    Random.Range(0, 5),
                    Random.Range(0, 5),
                },
                SelectedWall = new int[]
                {
                    Random.Range(0, 5),
                    Random.Range(0, 5),
                    Random.Range(0, 5),
                    Random.Range(0, 5),
                },
            });
            return rooms.ToList();
        }
        private void AddPossibleCells(GenerationMazeCell addedCell, List<GenerationMazeCell> possibleToCreate)
        {
            foreach (var side in SideExtensions.AllSides)
            {
                var newPosition = addedCell.Position + side.SideDirectionsInt();
                if (possibleToCreate.Any(cell => cell.Position == newPosition)) continue;
                possibleToCreate.Add(new GenerationMazeCell() { Depth = addedCell.Depth + 1, Position = newPosition, SideToAdd = side});
            }
        }
        private class GenerationMazeCell : MazeCell
        {
            public MazeCell Parent { get; init; }
            public Side SideToAdd { get; init; }
        }
    }
}