using System;
using Traps.Interfaces;
using UnityEngine;

namespace Entities.Components
{
    public class TrapsInput: MonoBehaviour,
        IDirtyTrapHandler, IDirtyTrapInput, ISpiresTrapHandler
    {
        [field: SerializeField] public bool TriggersSpikesTrap { get; private set; }
        public event Action<float> onTrapEnter;
        public event Action<float> onTrapExit;
        public void OnTrapEnter(float speedK)
        {
            onTrapEnter?.Invoke(speedK);
        }

        public void OnTrapExit(float speedK)
        {
            onTrapExit?.Invoke(speedK);
        }

    }

    public interface IDirtyTrapInput
    {
        event Action<float> onTrapEnter;
        event Action<float> onTrapExit;
    }
}