using NaughtyAttributes;
using UnityEngine;

namespace Entities.Components
{
    public class AnimatorOutput: MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [AnimatorParam(nameof(animator))] 
        [SerializeField] private string moveDirectionX;
        [AnimatorParam(nameof(animator))] 
        [SerializeField] private string moveDirectionY;
        [AnimatorParam(nameof(animator))] 
        [SerializeField] private string damaged;
        [AnimatorParam(nameof(animator))] 
        [SerializeField] private string died;

        public void ChangeMoveDir(Vector2 moveDir)
        {
            animator.SetFloat(moveDirectionX, moveDir.x);
            animator.SetFloat(moveDirectionY, moveDir.y);
        }
    }
}