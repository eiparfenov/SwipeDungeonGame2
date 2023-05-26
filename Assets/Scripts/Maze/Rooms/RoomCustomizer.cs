using System.Collections.Generic;
using Maze.Components;
using Zenject;

namespace Maze.Rooms
{
    public class RoomCustomizer: IInitializable
    {
        private readonly RoomInfo _roomInfo;
        private readonly MazeTheme _mazeTheme;
        private readonly List<RoomSide> _roomSides;
        private readonly RoomBackground _roomBackground;

        public RoomCustomizer(RoomInfo roomInfo, MazeTheme mazeTheme, List<RoomSide> roomSides, RoomBackground roomBackground)
        {
            _roomInfo = roomInfo;
            _mazeTheme = mazeTheme;
            _roomSides = roomSides;
            _roomBackground = roomBackground;
        }

        public void Initialize()
        {
            
        }
    }
}