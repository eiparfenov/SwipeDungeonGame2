using UnityEngine;

namespace Entities.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementOutput: MonoBehaviour
    {
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void ApplyVelocity(Vector2 velocity)
        {
            _rb.velocity = velocity;
        }

        public void ApplyPosition(Vector2 position)
        {
            transform.position = position;
        }
    }
}