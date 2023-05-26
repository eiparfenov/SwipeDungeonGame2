using Maze.Components;
using Maze.Rooms;
using UnityEngine;
using Zenject;

namespace Infrastructure.MazeInstallers
{
    public class RoomInstaller: Installer<RoomCreationDto, RoomInstaller>
    {
        private readonly RoomCreationDto _creationDto;

        public RoomInstaller(RoomCreationDto creationDto)
        {
            _creationDto = creationDto;
        }

        public override void InstallBindings()
        {
            Container.Bind<RoomInfo>().FromInstance(_creationDto.RoomInfo).AsCached();
            Container.Bind<Transform>().FromComponentOnRoot().AsCached();
            Container.Bind<RoomBackground>().FromComponentInHierarchy().AsCached();
            Container.Bind<RoomSide>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<RoomCustomizer>().AsCached();
            Container.Bind<Room>().AsCached();
        }
    }
}