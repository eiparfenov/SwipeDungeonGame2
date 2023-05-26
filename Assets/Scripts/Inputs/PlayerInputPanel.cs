using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils.Extensions;

namespace Inputs
{
    public class PlayerInputPanel: MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPlayerInput
    {
        public event Action onAttack;
        public event Action<Vector2> onDirectionChange;

        private Vector2 _dragStartPosition;

        public void OnPointerDown(PointerEventData eventData)
        {
            _dragStartPosition = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            var direction = eventData.position - _dragStartPosition;
            if(direction.magnitude < 50f)
            {
                onAttack?.Invoke();
                return;
            }
            direction = direction.SelectHorizontalVerticalAxis();
            onDirectionChange?.Invoke(direction);
        }
    }
}