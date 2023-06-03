using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Entities.Components;
using Maze.Components;
using Maze.Rooms;
using Traps.Interfaces;
using UnityEngine;
using Utils.Extensions;

namespace Traps.Components
{
    public class SawTrap: MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float knockBack;
        [SerializeField] private float speed;
        [SerializeField] private Transform[] sawPath;

        private RoomInteractiveBehaviour _roomInteractiveBehaviour;
        private void Start()
        {
            _roomInteractiveBehaviour = GetComponentInParent<RoomInteractiveBehaviour>();
            _roomInteractiveBehaviour.onRoomStart += RoomInteractiveBehaviourOnRoomStart;
        }

        private void RoomInteractiveBehaviourOnRoomStart()
        {
            MainLoop(this.GetCancellationTokenOnDestroy());
        }

        private async void MainLoop(CancellationToken cancellationToken)
        {
            var nextPoint = sawPath[0];
            var i = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                var direction = nextPoint.position - transform.position;
                var movement = direction.normalized * speed * Time.deltaTime;
                
                if (movement.magnitude > direction.magnitude)
                {
                    transform.position = nextPoint.position;
                    i = (i + 1) % sawPath.Length;
                    nextPoint = sawPath[i];
                }
                else
                {
                    transform.position += movement;
                }

                await UniTaskExt.ContinueOnCancel(() => UniTask.Yield(PlayerLoopTiming.Update, cancellationToken));
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var sawTrapHandler = col.GetComponent<ISawTrapHandler>();
            var damageInput = col.GetComponent<DamageInput>();
            
            if(damageInput == null || sawTrapHandler == null) return;
            
            if(!sawTrapHandler.TriggersSawTrap) return;
            
            damageInput.ApplyDamage(new()
            {
                Damage = damage, 
                Normal = (col.transform.position - transform.position).normalized,
                KnockBack = knockBack
            });
        }

        private void OnDestroy()
        {
            _roomInteractiveBehaviour.onRoomStart -= RoomInteractiveBehaviourOnRoomStart;
        }
    }
}