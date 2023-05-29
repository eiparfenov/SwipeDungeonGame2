using System;
using NaughtyAttributes;
using UnityEngine;

namespace Maze.Rooms
{
    public class RoomInteractiveBehaviour: MonoBehaviour
    {
        public Guid Id => Guid.Parse(serializedGuid);
        [HideInInspector][SerializeField]private string serializedGuid;

        public event Action onRoomStart;
        public event Action onRoomInitialized;
        public event Action onRoomLeft;
        public event Action onInteractiveFinished;
        
        [Button] public virtual void OnRoomInitialize() => onRoomInitialized?.Invoke();
        [Button] public virtual void OnRoomStart() => onRoomStart?.Invoke();
        [Button] public virtual void OnRoomLeft() => onRoomLeft?.Invoke();
        [Button] public virtual void OnInteractiveFinished() => onInteractiveFinished?.Invoke();
        protected void Reset()
        {
            serializedGuid = Guid.NewGuid().ToString();
        }

        [Button]
        void Check() => Debug.Log(Id);
    }
}
