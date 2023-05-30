using UnityEngine;
using Zenject;

namespace Utils.ZenjectExtras
{
    public abstract class SingleSettingsScriptableObjectInstaller<T>: ScriptableObjectInstaller
    {
        [SerializeField] private T settings;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<T>().FromInstance(settings).AsSingle();
        }
    }
}