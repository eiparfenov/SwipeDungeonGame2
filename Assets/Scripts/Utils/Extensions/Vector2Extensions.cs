using UnityEngine;

namespace Utils.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 SelectHorizontalVerticalAxis(this Vector2 source)
        {
            if (source == Vector2.zero) return Vector2.zero;

            if (Mathf.Abs(source.x) > Mathf.Abs(source.y))
            {
                return source.x > 0 ? Vector2.right : Vector2.left;
            }
            else
            {
                return source.y > 0 ? Vector2.up : Vector2.down;
            }
        }

        public static Vector2 SelectDiagonalAxis(this Vector2 source)
        {
            if (source == Vector2.zero) return Vector2.zero;
            
            var direction = Vector2.zero;

            direction += source.x > 0 ? Vector2.right : Vector2.left;
            direction += source.y > 0 ? Vector2.up : Vector2.down;

            return direction.normalized;
        }
    }
}