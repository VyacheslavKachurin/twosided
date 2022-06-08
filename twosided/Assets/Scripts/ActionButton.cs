using UnityEngine.EventSystems;
using System;
using UnityEngine;

public class ActionButton : MonoBehaviour,IPointerDownHandler
{
    public event Action<Direction> ButtonPressed;

    [SerializeField] Direction _direction;
    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonPressed?.Invoke(_direction);
    }
}
public enum Direction { Up, Down };
