using Entities.Components;
using Entities.DataObjects;
using Maze.Rooms;
using Traps.Interfaces;
using UnityEngine;
using Utils.Extensions;

namespace Traps.Components
{
    public class SpikesTrap: RoomInteractiveBehaviour
    {
        [SerializeField] private Sprite idle;
        [SerializeField] private Sprite attack;
        [SerializeField] private float damage;
        [SerializeField] private float delayAfterStep;
        [SerializeField] private float delayAfterAttack;

        private BoxCollider2D _boxCollider2D;
        private SpriteRenderer _spriteRenderer;
        
        private bool _loaded;

        private void Start()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private async void OnTriggerEnter2D(Collider2D col)
        {
            if(_loaded) return;
            var spikesTrapHandler = col.GetComponent<ISpikesTrapHandler>();
            if(spikesTrapHandler == null || !spikesTrapHandler.TriggersSpikesTrap) return;

            _loaded = true;
            await UniTaskExt.Delay(delayAfterStep);
            if(!this) return;
            
            _spriteRenderer.sprite = attack;
            var colliders = Physics2D.OverlapBoxAll(transform.position, _boxCollider2D.size, 0);
            foreach (var toDamage in colliders)
            {
                var damageInput = toDamage.GetComponent<DamageInput>();
                if(!damageInput) continue;
                
                damageInput.ApplyDamage(new DamageInfo(){Damage = damage});
            }

            await UniTaskExt.Delay(delayAfterAttack);
            if(!this) return;
            _spriteRenderer.sprite = idle;
            _loaded = false;
        }
    }
}