using Maze.Rooms;
using Zenject;

namespace Infrastructure.MazeInstallers
{
    public record RoomCreationDto(RoomInfo RoomInfo);
    public class RoomFactory: PlaceholderFactory<RoomCreationDto, Room>
    {
        
    }
}