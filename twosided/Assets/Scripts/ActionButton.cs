using UnityEngine.EventSystems;
using System;
using UnityEngine;

public class ActionButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public event Action<Direction> ButtonPressed;
    public event Action ButtonUp;

    [SerializeField] Direction _direction;
    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonPressed?.Invoke(_direction);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonUp?.Invoke();
    }
}
public enum Direction { Up, Down };
