using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Utils.ZenjectExtras
{
    public static class DiContainerExtensions
    {
        public static void AddZenjectDynamicInterfaces(this DiContainer container)
        {
            container.Bind<ZenjectDynamicInterfaces>().FromNewComponentOnRoot().AsCached().NonLazy();
        }

        public static void AddTransformFromRoot(this DiContainer container)
        {
            container.Bind<Transform>().FromNewComponentOnRoot().AsCached();
        }

        public static void AddCancellationTokenFromTransformOnRoot(this DiContainer container)
        {
            if (!container.HasBinding<Transform>())
            {
                container.Bind<Transform>().FromNewComponentOnRoot().AsCached();
            }

            container.Bind<CancellationToken>()
                .FromMethod(() => container.Resolve<Transform>().GetCancellationTokenOnDestroy());
        }
    }
}