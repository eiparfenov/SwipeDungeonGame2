using Entities.Components;
using Entities.Movement;
using Entities.Movement.MovementStrategies;
using Entities.Stats;
using Inputs;
using UnityEngine;
using Zenject;

namespace Infrastructure.SceneMonoInstallers
{
    public class PlayerMonoInstaller: MonoInstaller
    {
        [SerializeField] private MovementOutput movementOutput;
        [SerializeField] private PlayerInputPanel playerInputPanel;
        [SerializeField] private HitInput hitInput;
        [SerializeField] private StartEntityStats startEntityStats;
        public override void InstallBindings()
        {
            Container.Bind<StartEntityStats>().FromInstance(startEntityStats).AsCached();
            Container.Bind<HitInput>().FromInstance(hitInput).AsCached();
            Container.Bind<EntityStats>().AsCached();
            Container.BindInstance(movementOutput).AsCached();
            Container.Bind<IPlayerInput>().FromInstance(playerInputPanel).AsCached();
            
            Container.BindInterfacesAndSelfTo<PlayerInputMovementStrategy>().AsCached();
            Container.BindInterfacesAndSelfTo<MovementController>().AsCached().NonLazy();
        }
    }
}