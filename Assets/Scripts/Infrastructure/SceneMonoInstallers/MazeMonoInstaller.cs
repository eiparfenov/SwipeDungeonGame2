﻿using Infrastructure.MazeInstallers;
using Maze.Generation;
using Maze.Rooms;
using UnityEngine;
using Zenject;

namespace Infrastructure.SceneMonoInstallers
{
    public class MazeMonoInstaller: MonoInstaller
    {
        [SerializeField] private GameObject roomPref;
        [SerializeField] private MazeTheme currentMazeTheme;
        [SerializeField] private MazeGenerationSettings mazeGenerationSettings;
        [SerializeField] private RoomSettings roomSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<MazeGenerationSettings>().FromInstance(mazeGenerationSettings).AsCached();
            Container.Bind<RoomSettings>().FromInstance(roomSettings).AsCached();
            Container.Bind<MazeTheme>().FromInstance(currentMazeTheme).AsCached();
            
            Container.BindFactory<RoomCreationDto, Room, RoomFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<RoomInstaller>(roomPref)
                .UnderTransformGroup("Rooms");
            
            Container.BindInterfacesAndSelfTo<MazeGenerator>().AsCached();
            Container.BindInterfacesAndSelfTo<RoomsLoader>().AsCached().NonLazy();
        }
    }
}