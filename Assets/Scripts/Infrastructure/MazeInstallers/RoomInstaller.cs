using Maze.Components;
using Maze.Rooms;
using UnityEngine;
using Utils.ZenjectExtras;
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
            Container.Bind<RoomInfo>().FromMethod(() => _creationDto.RoomInfo).AsCached();
            Container.AddCancellationTokenFromTransformOnRoot();
            Container.Bind<RoomBackground>().FromComponentInHierarchy().AsCached();
            Container.Bind<RoomSide>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<RoomWallsCustomizer>().AsCached();
            Container.BindInterfacesAndSelfTo<Room>().AsCached();
            Container.AddZenjectDynamicInterfaces();
        }
    }
}