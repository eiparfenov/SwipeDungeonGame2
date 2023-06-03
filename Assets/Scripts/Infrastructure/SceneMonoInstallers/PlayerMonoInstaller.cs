using Entities.Components;
using Entities.Movement;
using Entities.Movement.MovementStrategies;
using Entities.Stats;
using Inputs;
using Maze;
using UnityEngine;
using Utils.ZenjectExtras;
using Zenject;

namespace Infrastructure.SceneMonoInstallers
{
    public class PlayerMonoInstaller: MonoInstaller
    {
        [SerializeField] private StartEntityStats startEntityStats;
        public override void InstallBindings()
        {
            Container.AddCancellationTokenFromTransformOnRoot();
            
            // Binds stats
            Container.Bind<StartEntityStats>().FromInstance(startEntityStats).AsCached();
            Container.Bind<EntityStats>().AsCached();
            
            // Binds inputs
            Container.BindInterfacesAndSelfTo<TrapsInput>().FromComponentInHierarchy().AsCached();
            Container.Bind<HitInput>().FromComponentInHierarchy().AsCached();
            Container.Bind<IPlayerInput>().FromComponentInHierarchy().AsCached();
            Container.Bind<DamageInput>().FromComponentInHierarchy().AsCached();
            
            // Binds outputs
            Container.BindInterfacesAndSelfTo<MovementOutput>().FromComponentInHierarchy().AsCached();
            
            // Binds movement strategies
            Container.BindInterfacesAndSelfTo<MovementLockOnRoomChange>().AsCached();
            Container.BindInterfacesAndSelfTo<PlayerInputMovementStrategy>().AsCached();
            Container.BindInterfacesAndSelfTo<KnockBackVelocityEffector>().AsCached();
            Container.BindInterfacesAndSelfTo<DirtyTrapVelocityEffector>().AsCached();
            Container.BindInterfacesAndSelfTo<LeavesTrapProcessor>().AsCached();
            Container.BindInterfacesAndSelfTo<MovementController>().AsCached().NonLazy();
        }
    }
}