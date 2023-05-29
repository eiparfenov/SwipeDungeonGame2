using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Entities.Components;
using Entities.DataObjects;
using UnityEngine;
using Utils.Extensions;
using Zenject;

namespace Entities.Movement.MovementStrategies
{
    public class KnockBackVelocityEffector: IMovementStrategy, IInitializable, IDisposable
    {
        private class AdditionalVelocityHolder
        {
            public float Progress { get; set; }

            public Vector2 Value
            {
                get => _startValue * Progress;
                set 
                { 
                    _startValue = value;
                    Progress = 1;
                }
            }
            private Vector2 _startValue;
            
        }
        
        private readonly DamageInput _damageInput;
        private readonly CancellationToken _cancellationToken;
        private List<AdditionalVelocityHolder> _additionalVelocity;

        public KnockBackVelocityEffector(DamageInput damageInput, CancellationToken cancellationToken)
        {
            _damageInput = damageInput;
            _cancellationToken = cancellationToken;
            _additionalVelocity = new();
        }

        public int Order => 1;
        public void GetVelocity(ref Vector3 velocity)
        {
            var additionalVelocity = Vector2.zero;
            _additionalVelocity.Foreach(x => additionalVelocity += x.Value);
            velocity += (Vector3)additionalVelocity;
        }

        public void Initialize()
        {
            _damageInput.onDamageInput += DamageInputOnDamageInput;
        }

        private void DamageInputOnDamageInput(DamageInfo damageInfo)
        {
            ProcessVelocity(damageInfo.Normal, damageInfo.KnockBack, _cancellationToken);
        }

        private async void ProcessVelocity(Vector2 normal, float knockBack, CancellationToken cancellationToken)
        {
            var additionalVelocity = new AdditionalVelocityHolder() { Value = normal * knockBack };
            _additionalVelocity.Add(additionalVelocity);
            while (!cancellationToken.IsCancellationRequested)
            {
                additionalVelocity.Progress -= Time.deltaTime;
                if (additionalVelocity.Progress < 0)
                {
                    _additionalVelocity.Remove(additionalVelocity);
                    return;
                }
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        public void Dispose()
        {
            _damageInput.onDamageInput -= DamageInputOnDamageInput;
        }
    }

}