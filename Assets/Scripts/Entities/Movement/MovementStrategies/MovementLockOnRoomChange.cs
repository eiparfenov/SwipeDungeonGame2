using System;
using Cysharp.Threading.Tasks;
using Entities.Stats;
using Shared;
using UnityEngine;
using Utils.Extensions;
using Zenject;

namespace Entities.Movement.MovementStrategies
{
    public class MovementLockOnRoomChange: IMovementStrategy, IInitializable, IDisposable
    {
        private readonly GlobalSettings _globalSettings;
        private readonly SignalBus _signal;
        private readonly EntityStats _entityStats;

        private Vector2? keepVelocity;
        public MovementLockOnRoomChange(GlobalSettings globalSettings, SignalBus signal, EntityStats entityStats)
        {
            _globalSettings = globalSettings;
            _signal = signal;
            _entityStats = entityStats;
        }

        public int Order => 20;
        public void GetVelocity(ref Vector3 velocity)
        {
            if (keepVelocity.HasValue)
                velocity = keepVelocity.Value;
            
        }

        public void Initialize()
        {
            _signal.Subscribe<RoomChanged>(RoomOnChanged);
        }

        private async void RoomOnChanged(RoomChanged signal)
        {
            keepVelocity = _entityStats.Velocity.Value;
            await UniTaskExt.Delay(_globalSettings.RoomExitTime);
            keepVelocity = null;
        }

        public void Dispose()
        {
            _signal.Unsubscribe<RoomChanged>(RoomOnChanged);
        }
    }
}