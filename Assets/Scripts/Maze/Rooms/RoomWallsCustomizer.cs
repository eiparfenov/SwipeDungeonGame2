using System.Collections.Generic;
using System.Linq;
using Maze.Components;
using Shared;
using UnityEngine;
using Zenject;

namespace Maze.Rooms
{
    public class RoomWallsCustomizer: IInitializable
    {
        private readonly RoomInfo _roomInfo;
        private readonly MazeTheme _mazeTheme;
        private readonly List<RoomSide> _roomSides;
        private readonly RoomBackground _roomBackground;
        

        public RoomWallsCustomizer(RoomInfo roomInfo, MazeTheme mazeTheme, List<RoomSide> roomSides, RoomBackground roomBackground)
        {
            _roomInfo = roomInfo;
            _mazeTheme = mazeTheme;
            _roomSides = roomSides.OrderBy(x => (int)x.Side).ToList();
            _roomBackground = roomBackground;
        }

        public void Initialize()
        {
            for (int i = 0; i < 4; i++)
            {
                var roomSide = _roomSides[i];
                roomSide.SetWall(!roomSide.Side.IsHorizontal()?
                    _mazeTheme.VerticalDoors[_roomInfo.SelectedWall[i]]:
                    _mazeTheme.HorizontalDoors[_roomInfo.SelectedWall[i]]);
                if (_roomInfo.Gates.Contains(roomSide.Side))
                {
                    var roomGateId = _roomInfo.SelectedGate[i];
                    roomSide.SetGate(_mazeTheme.GateBottoms[roomGateId], _mazeTheme.GateDoors[roomGateId], _mazeTheme.GateTops[roomGateId]);
                }
            }
        }
    }
}