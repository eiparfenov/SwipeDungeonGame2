using System;
using UnityEngine;
using Utils.Extensions;
using Zenject;
using Object = UnityEngine.Object;

namespace Entities.Handlers
{
    public class DieHandler: IInitializable, IDisposable
    {
        private readonly DamageHandler _damageHandler;
        private readonly Transform _transform;

        public DieHandler(DamageHandler damageHandler, Transform transform)
        {
            _damageHandler = damageHandler;
            _transform = transform;
        }

        public void Initialize()
        {
            _damageHandler.onEntityDie += DamageHandlerOnEntityDie;
        }

        protected virtual async void DamageHandlerOnEntityDie()
        {
            // play animation
            await UniTaskExt.Delay(1f);
            Object.Destroy(_transform.gameObject);
        }

        public void Dispose()
        {
            _damageHandler.onEntityDie -= DamageHandlerOnEntityDie;
        }
    }
}