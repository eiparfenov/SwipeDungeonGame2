using UnityEngine;

namespace Entities.Components
{
    public interface IAnimatorOutput
    {
        void ChangeMoveDir(Vector2 moveDir);
    }
}