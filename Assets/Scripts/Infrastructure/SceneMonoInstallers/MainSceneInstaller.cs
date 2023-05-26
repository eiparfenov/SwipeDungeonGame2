using UnityEngine;
using Zenject;

namespace Infrastructure.SceneMonoInstallers
{
    public class MainSceneInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromComponentInHierarchy().AsCached();
        }
    }
}