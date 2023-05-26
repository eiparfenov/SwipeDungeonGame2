using System;
using Entities.DataObjects;
using UnityEngine;
using Zenject;

namespace Entities.Components
{
    [RequireComponent(typeof(Collider2D))]
    public class DamageInput: MonoBehaviour
    {
        public EntityType EntityType { get; private set; }
        public event Action<DamageInfo> onDamageInput;
        
        [Inject]
        public void Construct(EntityType entityType)
        {
            EntityType = entityType;
        }

        public void ApplyDamage(DamageInfo damageInfo)
        {
            onDamageInput?.Invoke(damageInfo);
        }
    }
}