using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InputController : MonoBehaviour
{


    public event Action<Direction> UpButtonPressed;
    public event Action<Direction> DownButtonPressed;

    [SerializeField] private Button _topButton;
    [SerializeField] private Button _downButton;

    private void Start()
    {
        _topButton.onClick.AddListener(MoveUp);
        _downButton.onClick.AddListener(MoveDown);
    }

    private void MoveUp() => UpButtonPressed(Direction.Up);

    private void MoveDown() => DownButtonPressed(Direction.Down);

}
public enum Direction { Up, Down };
