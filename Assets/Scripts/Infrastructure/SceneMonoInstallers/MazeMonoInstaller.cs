using Infrastructure.MazeInstallers;
using Maze.Generation;
using Maze.Rooms;
using UnityEngine;
using Zenject;

namespace Infrastructure.SceneMonoInstallers
{
    public class MazeMonoInstaller: MonoInstaller
    {
        [SerializeField] private GameObject roomPref;
        public override void InstallBindings()
        {
            Container.BindFactory<RoomCreationDto, Room, RoomFactory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<RoomInstaller>(roomPref)
                .UnderTransformGroup("Rooms");
            
            Container.BindInterfacesAndSelfTo<MazeGenerator>().AsCached();
        }
    }
}