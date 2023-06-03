using System;
using Entities.Components;
using Entities.DataObjects;
using Maze.Components;
using Maze.Rooms;
using Traps.Interfaces;
using UnityEngine;

namespace Traps.Components
{
    public class LeavesTrap: RoomInteractiveBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float timeInside;
        [SerializeField] private float escapeRange;

        [SerializeField] private Sprite spikesSprite;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var trapHandler = col.GetComponent<ILeavesTrapHandler>();
            if(trapHandler == null) return;

            _spriteRenderer.sprite = spikesSprite;
            
            trapHandler.OnTrapEnter(new LeavesTrapEnterDto()
            {
                TrapPosition = transform.position,
                TrappedTime = timeInside,
                TrapEscapeRange = escapeRange,
            });

            var damageInput = col.GetComponent<DamageInput>();
            if(damageInput) damageInput.ApplyDamage(new DamageInfo(){Damage = damage});
        }
    }
}