using UnityEngine;

namespace Maze.Components
{
    public class RoomBackground: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer background;

        public void SetBackground(Sprite backgroundSprite) => background.sprite = backgroundSprite;
    }
}