using Shared;
using Zenject;

namespace Infrastructure.SceneMonoInstallers
{
    public class GlobalSignalsInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<RoomChanged>().OptionalSubscriber();
            Container.DeclareSignal<PlayerDied>().OptionalSubscriber();
        }
    }
}