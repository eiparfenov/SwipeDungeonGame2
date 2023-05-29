using System;
using Entities.DataObjects;
using UnityEngine;
using Zenject;

namespace Entities.Components
{
    [RequireComponent(typeof(Collider2D))]
    public class DamageInput: MonoBehaviour
    {
        [field: SerializeField] public EntityType EntityType { get; private set; }
        public event Action<DamageInfo> onDamageInput;
        

        public void ApplyDamage(DamageInfo damageInfo)
        {
            onDamageInput?.Invoke(damageInfo);
            Debug.Log($"Damaged {damageInfo.Damage}");
        }
    }
}