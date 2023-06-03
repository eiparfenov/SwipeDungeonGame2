using System;
using System.Linq;
using Maze.Components;
using NaughtyAttributes;
using UnityEngine;

namespace Maze.Rooms
{
    [CreateAssetMenu(menuName = "SwipeDungeon/Maze/Room")]
    public class RoomCreationData: ScriptableObject
    {
        [field: SerializeField] public RoomContentInfo[] RoomContentInfos { get; private set; }
        [field: SerializeField] public int WallId { get; private set; }
        [field: SerializeField] public int GateId { get; private set; }

        private void Reset()
        {
            Debug.Log(FindObjectsOfType<RoomInteractiveBehaviour>().Length);
            RoomContentInfos = FindObjectsOfType<RoomInteractiveBehaviour>()
                .Select(rib => new RoomContentInfo()
                {
                    Name = rib.name,
                    RoomBehaviour = rib.Id,
                    Position = rib.transform.localPosition
                })
                .ToArray();
            WallId = -1;
            GateId = -1;
        }
    }
}