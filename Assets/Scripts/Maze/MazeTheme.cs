using System;
using System.Linq;
using Maze.Rooms;
using NaughtyAttributes;
using UnityEngine;

namespace Maze
{
    [CreateAssetMenu(menuName = "SwipeDungeon/Maze/MazeTheme")]
    public class MazeTheme: ScriptableObject
    {
        #region Sprites
        
        [field: Foldout("Sprites")] [field: SerializeField] public Sprite[] VerticalDoors { get; private set; }
        [field: Foldout("Sprites")] [field: SerializeField] public Sprite[] HorizontalDoors { get; private set; }
        [field: Foldout("Sprites")] [field: SerializeField] public Sprite[] GateTops { get; private set; }
        [field: Foldout("Sprites")] [field: SerializeField] public Sprite[] GateBottoms { get; private set; }
        [field: Foldout("Sprites")] [field: SerializeField] public Sprite[] GateDoors { get; private set; }
        
        #endregion
        [field: Space]
        [field: SerializeField] public RoomCreationInstruction[] RoomCreationInstructions { get; private set; }

        public int RoomsCount => RoomCreationInstructions
            .Select(instr => instr.CreateOneInstanceOfAll ? instr.Rooms.Length : instr.RoomsCount)
            .Sum();
    }

    [Serializable]
    public class RoomCreationInstruction
    {
        public enum Place{Random, Start, End}
        [field: SerializeField] public Place GenerationPlace { get; private set; }
        [field: SerializeField] public int GenerationOffset { get; private set; }
        
        [field: Space]
        [field: SerializeField] public bool CreateOneInstanceOfAll { get; private set; }
        [field: SerializeField] public int RoomsCount { get; private set; }
        
        [field: Space]
        [field: SerializeField] public RoomCreationDataWithProbability[] Rooms { get; private set; }
    }

    [Serializable]
    public class RoomCreationDataWithProbability
    {
        [field: SerializeField] public float Weight { get; private set; }
        [field: SerializeField] public RoomCreationData Room { get; private set; }
    }
}