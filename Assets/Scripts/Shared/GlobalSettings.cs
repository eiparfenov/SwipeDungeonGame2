using System;
using UnityEngine;

namespace Shared
{
    [Serializable]
    public class GlobalSettings
    {
        [field: SerializeField] public float RoomExitTime { get; private set; }
        [field: SerializeField] public Vector2 RoomSize { get; private set; }
        [field: SerializeField] public Vector2 RoomOffset { get; private set; }
    }
}