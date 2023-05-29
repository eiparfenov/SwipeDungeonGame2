using System;
using Entities.Components;
using UnityEngine;
using Zenject;

namespace Entities.Movement.MovementStrategies
{
    public class DirtyTrapVelocityEffector: IMovementStrategy, IInitializable, IDisposable
    {
        private readonly IDirtyTrapInput _dirtyTrapInput;

        public DirtyTrapVelocityEffector(IDirtyTrapInput dirtyTrapInput)
        {
            _dirtyTrapInput = dirtyTrapInput;
        }

        private float _speedK = 1;
        public int Order => 1;
        public void GetVelocity(ref Vector3 velocity)
        {
            velocity *= _speedK;
        }

        public void Initialize()
        {
            _dirtyTrapInput.onTrapExit += DirtyTrapInputOnTrapExit;
            _dirtyTrapInput.onTrapEnter += DirtyTrapInputOnTrapEnter;
        }

        private void DirtyTrapInputOnTrapEnter(float speedK)
        {
            _speedK *= speedK;
        }

        private void DirtyTrapInputOnTrapExit(float speedK)
        {
            _speedK /= speedK;
        }

        public void Dispose()
        {
            _dirtyTrapInput.onTrapExit -= DirtyTrapInputOnTrapExit;
            _dirtyTrapInput.onTrapEnter -= DirtyTrapInputOnTrapEnter;
        }
    }
}