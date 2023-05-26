using UnityEngine;

namespace Maze.Rooms
{
    [CreateAssetMenu(menuName = "SwipeDungeon/Maze/MazeTheme")]
    public class MazeTheme: ScriptableObject
    {
        [field: SerializeField] public Sprite[] VerticalDoors { get; private set; }
        [field: SerializeField] public Sprite[] HorizontalDoors { get; private set; }
        [field: SerializeField] public Sprite[] GateTops { get; private set; }
        [field: SerializeField] public Sprite[] GateBottoms { get; private set; }
        [field: SerializeField] public Sprite[] GateDoors { get; private set; }
    }
}