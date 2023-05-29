using System;
using System.Threading;
using Entities.Components;
using Entities.Movement.MovementStrategies;
using Entities.Stats;
using Traps.Interfaces;
using UnityEngine;
using Utils.Extensions;
using Zenject;

namespace Entities.Movement
{
    public class LeavesTrapProcessor: IMovementStrategy, IInitializable, IDisposable
    {
        private readonly MovementOutput _movementOutput;
        private readonly EntityStats _entityStats;
        private readonly CancellationToken _cancellationToken;
        private readonly ILeavesTrapInput _leavesTrapInput;

        public LeavesTrapProcessor(MovementOutput movementOutput, EntityStats entityStats, CancellationToken cancellationToken, ILeavesTrapInput leavesTrapInput)
        {
            _movementOutput = movementOutput;
            _entityStats = entityStats;
            _cancellationToken = cancellationToken;
            _leavesTrapInput = leavesTrapInput;
        }

        public int Order => 10;
        private bool _trapped;
        public void GetVelocity(ref Vector3 velocity)
        {
            if(_trapped)
                velocity = Vector3.zero;
        }

        public void Initialize()
        {
            _leavesTrapInput.onLeavesTrapEnter += LeavesTrapInputOnLeavesTrapEnter;
        }

        private void LeavesTrapInputOnLeavesTrapEnter(LeavesTrapEnterDto obj)
        {
            ProcessTrap(obj, _cancellationToken);
        }

        private async void ProcessTrap(LeavesTrapEnterDto leavesTrapEnterDto, CancellationToken cancellationToken)
        {
            if(_trapped) return;

            _trapped = true;
            var velocity = _entityStats.Velocity.Value.normalized;
            _movementOutput.ApplyPosition(leavesTrapEnterDto.TrapPosition);
            
            await UniTaskExt.ContinueOnCancel(() =>
                UniTaskExt.Delay(leavesTrapEnterDto.TrappedTime, cancellationToken: cancellationToken));
            
            _movementOutput.ApplyPosition((Vector2)leavesTrapEnterDto.TrapPosition + (velocity * leavesTrapEnterDto.TrapEscapeRange));
            _trapped = false;
        }

        public void Dispose()
        {
            _leavesTrapInput.onLeavesTrapEnter -= LeavesTrapInputOnLeavesTrapEnter;
        }
    }
}