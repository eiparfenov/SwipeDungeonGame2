using System;
using Traps.Interfaces;
using UnityEngine;

namespace Entities.Components
{
    public class TrapsInput: MonoBehaviour,
        IDirtyTrapHandler, IDirtyTrapInput, 
        ISpikesTrapHandler, 
        ISawTrapHandler, 
        ILeavesTrapHandler, ILeavesTrapInput
    {
        [field: SerializeField] public bool TriggersSpikesTrap { get; private set; }
        [field: SerializeField] public bool TriggersSawTrap { get; private set; }
        public event Action<float> onDirtyTrapEnter;
        public event Action<float> onDirtyTrapExit;
        public event Action<LeavesTrapEnterDto> onLeavesTrapEnter;
        public void OnTrapEnter(float speedK)
        {
            onDirtyTrapEnter?.Invoke(speedK);
        }

        public void OnTrapExit(float speedK)
        {
            onDirtyTrapExit?.Invoke(speedK);
        }

        public void OnTrapEnter(LeavesTrapEnterDto leavesTrapEnterDto)
        {
            onLeavesTrapEnter?.Invoke(leavesTrapEnterDto);
        }

    }

    public interface IDirtyTrapInput
    {
        event Action<float> onDirtyTrapEnter;
        event Action<float> onDirtyTrapExit;
    }

    public interface ILeavesTrapInput
    {
        event Action<LeavesTrapEnterDto> onLeavesTrapEnter;
    }
}