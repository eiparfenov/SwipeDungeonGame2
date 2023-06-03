using System.Collections.Generic;
using System.Linq;
using Maze.Rooms;
using UnityEngine;
using Utils.Extensions;

namespace Maze.Generation
{
    public class StdMazeInfoGenerator: MazeInfoGeneratorBase
    {
        public StdMazeInfoGenerator(MazeTheme mazeTheme) : base(mazeTheme)
        {
        }

        public override List<RoomInfo> GenerateRooms(List<MazeCell> mazeCells)
        {
            var result = new List<RoomInfo>();
            
            var rooms = MazeTheme.RoomCreationInstructions
                .SelectMany(instruction => instruction.Rooms
                    .Select( room => new RoomWithPlacementRules()
                    {
                        Offset = instruction.GenerationOffset,
                        Place = instruction.GenerationPlace,
                        RoomCreationData = instruction.CreateOneInstanceOfAll? 
                            room.Room:
                            instruction.Rooms.RandomOrDefaultWithWeight(x => x.Weight).Room
                    })
                )
                .ToList();

            var startRooms = rooms
                .Where(room => room.Place == RoomCreationInstruction.Place.Start)
                .OrderBy(room => room.Offset);

            foreach (var startRoom in startRooms)
            {
                var cellToAdd = mazeCells.ItemWithMin(x => Mathf.Abs(x.Depth - startRoom.Offset));
                mazeCells.Remove(cellToAdd);
                result.Add(new RoomInfo(cellToAdd)
                {
                    SelectedGate = new []
                    {
                        GateWallId(startRoom.RoomCreationData.GateId),
                        GateWallId(startRoom.RoomCreationData.GateId),
                        GateWallId(startRoom.RoomCreationData.GateId),
                        GateWallId(startRoom.RoomCreationData.GateId),
                    },
                    SelectedWall = new[]
                    {
                        GateWallId(startRoom.RoomCreationData.WallId),
                        GateWallId(startRoom.RoomCreationData.WallId),
                        GateWallId(startRoom.RoomCreationData.WallId),
                        GateWallId(startRoom.RoomCreationData.WallId),
                    },
                    RoomContents = startRoom.RoomCreationData.RoomContentInfos.ToList(),
                });
            }

            var endRooms = rooms
                .Where(room => room.Place == RoomCreationInstruction.Place.End)
                .OrderByDescending(room => room.Offset);

            foreach (var endRoom in endRooms)  
            {
                var cellToAdd = mazeCells.Where(cell => cell.IsCorner)
                    .ItemWithMax();
                mazeCells.Remove(cellToAdd);
                result.Add(new RoomInfo(cellToAdd)
                {
                    SelectedGate = new []
                    {
                        GateWallId(startRoom.RoomCreationData.GateId),
                        GateWallId(startRoom.RoomCreationData.GateId),
                        GateWallId(startRoom.RoomCreationData.GateId),
                        GateWallId(startRoom.RoomCreationData.GateId),
                    },
                    SelectedWall = new[]
                    {
                        GateWallId(startRoom.RoomCreationData.WallId),
                        GateWallId(startRoom.RoomCreationData.WallId),
                        GateWallId(startRoom.RoomCreationData.WallId),
                        GateWallId(startRoom.RoomCreationData.WallId),
                    },
                    RoomContents = startRoom.RoomCreationData.RoomContentInfos.ToList(),
                });
            }

            return result;
        }

        private int GateWallId(int gateWallId)
        {
            return gateWallId != -1?
                gateWallId:
                Random.Range(0, MazeTheme.GateBottoms.Length);
        }
    }

    public class RoomWithPlacementRules
    {
        public RoomCreationData RoomCreationData { get; init; }
        public RoomCreationInstruction.Place Place { get; init; }
        public int Offset { get; init; }
    }
}