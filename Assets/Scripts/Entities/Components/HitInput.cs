using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Entities.Components
{
    public class HitInput: MonoBehaviour
    {
        private bool _delayBool;
        public event Action<Vector2> onHit;
        

        private async void OnCollisionEnter2D(Collision2D col)
        {
            if(_delayBool) return;
            _delayBool = true;
            onHit?.Invoke(col.contacts[0].normal);
            await UniTask.Yield(PlayerLoopTiming.Update);
            _delayBool = false;
        }
    }
}