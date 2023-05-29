using System;
using Shared;
using UnityEngine;

namespace Maze.Components
{
    public class RoomSide: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer wall;
        [SerializeField] private SpriteRenderer gateBottom;
        [SerializeField] private SpriteRenderer gateDoor;
        [SerializeField] private SpriteRenderer gateTop;
        [SerializeField] private Collider2D doorCollider;
        [field: SerializeField] public Side Side { get; private set; }

        public event Action<Side> onRoomExit;

        public void SetWall(Sprite wallSprite)
        {
            wall.sprite = wallSprite;
        }

        public void SetGate(Sprite gateBottomSprite, Sprite gateDoorSprite, Sprite gateTopSprite)
        {
            gateBottom.enabled = true;
            gateTop.enabled = true;
            
            gateBottom.sprite = gateBottomSprite;
            gateDoor.sprite = gateDoorSprite;
            gateTop.sprite = gateTopSprite;
        }

        public void SetDoorState(bool opened)
        {
            gateDoor.enabled = !opened;
            doorCollider.enabled = !opened;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            onRoomExit?.Invoke(Side);
            Debug.Log($"Exited {Side}");
        }
    }
}