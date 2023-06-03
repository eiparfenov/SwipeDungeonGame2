using System;
using Entities.Components;
using Entities.DataObjects;
using Entities.Stats;
using Zenject;

namespace Entities
{
    public class DamageHandler: IInitializable, IDisposable
    {
        private readonly DamageInput _damageInput;
        private readonly EntityStats _entityStats;
        public event Action onEntityDie; 
        public void Initialize()
        {
            _damageInput.onDamageInput += DamageInputOnDamageInput;
        }

        private void DamageInputOnDamageInput(DamageInfo damage)
        {
            _entityStats.Health.Value -= damage.Damage;
            // cause health is mutable life data after it's change some items can chan it

            if (_entityStats.Health.Value <= 0f)
            {
                onEntityDie?.Invoke();
            }
        }

        public void Dispose()
        {
            _damageInput.onDamageInput -= DamageInputOnDamageInput;
        }
    }
}