using System;
using UnityEngine;

namespace Entities.Components
{
    public class HitInput: MonoBehaviour
    {
        public event Action<Vector2> onHit;

        private void OnCollisionEnter2D(Collision2D col)
        {
            onHit?.Invoke(col.contacts[0].normal);
        }
    }
}