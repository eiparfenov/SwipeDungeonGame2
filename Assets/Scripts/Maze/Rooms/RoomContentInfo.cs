using System;
using UnityEngine;

namespace Maze.Rooms
{
    [Serializable]
    public class RoomContentInfo
    {
        public Guid RoomBehaviour 
        { 
            get => Guid.Parse(serializedGuid);
            init => serializedGuid = value.ToString();
        }

        public Vector2 Position
        {
            get => serializedPosition;
            init => serializedPosition = value;
        }
        public string Name
        {
            get => name;
            init => name = value;
        }

        [HideInInspector][SerializeField] private string name;
        [SerializeField] private string serializedGuid;
        [SerializeField] private Vector2 serializedPosition;
    }
}