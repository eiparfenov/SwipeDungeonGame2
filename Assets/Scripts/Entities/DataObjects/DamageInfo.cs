using UnityEngine;

namespace Entities.DataObjects
{
    public class DamageInfo
    {
        public float Damage { get; init; }
        public float KnockBack { get; init; } 
        public Vector3 Normal { get; init; }
    }
}