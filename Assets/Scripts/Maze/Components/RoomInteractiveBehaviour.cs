using System;
using NaughtyAttributes;
using UnityEngine;

namespace Maze.Components
{
    public class RoomInteractiveBehaviour: MonoBehaviour
    {
        public Guid Id => Guid.Parse(serializedGuid);
        [HideInInspector][SerializeField]private string serializedGuid;
        
        [field: SerializeField] public bool IsInteractiveFinished { get; set; }
        [field: SerializeField] public bool LoadInteractiveOnNextRoomLoad { get; set; }
        
        public event Action onRoomStart;
        public event Action onRoomInitialized;
        public event Action onRoomLeft;
        
        [Button] public virtual void OnRoomInitialize() => onRoomInitialized?.Invoke();
        [Button] public virtual void OnRoomStart() => onRoomStart?.Invoke();
        [Button] public virtual void OnRoomLeft() => onRoomLeft?.Invoke();
        protected void Reset()
        {
            serializedGuid = Guid.NewGuid().ToString();
        }
        [Button]
        void Check() => Debug.Log(Id);
    }
}
