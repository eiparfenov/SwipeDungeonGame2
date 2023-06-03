using System;
using Entities.Handlers;
using Shared;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    public class PlayerDieHandler: DieHandler
    {
        private readonly SignalBus _signalBus;

        public PlayerDieHandler(DamageHandler damageHandler, Transform transform, SignalBus signalBus) : base(damageHandler, transform)
        {
            _signalBus = signalBus;
        }

        protected override void DamageHandlerOnEntityDie()
        {
            base.DamageHandlerOnEntityDie();
            _signalBus.Fire<PlayerDied>();
        }
    }
}