using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils.Extensions;
using Zenject;

namespace Utils.ZenjectExtras
{
    public class ZenjectDynamicInterfaces: MonoBehaviour
    {
        [InjectLocal] private List<IInitializable> _initializables;

        [InjectLocal] private List<ITickable> _tickables;
        
        [InjectLocal] private List<IDisposable> _disposables;

        private void Start()
        {
            _initializables.ForEach(initializable => initializable.Initialize());
        }

        private void Update()
        {
            _tickables.ForEach(tickable => tickable.Tick());
        }

        private void OnDestroy()
        {
            _disposables.Foreach(disposable => disposable.Dispose());
        }
    }
}