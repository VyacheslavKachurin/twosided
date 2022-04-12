using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public event Action<Direction> UpButtonPressed;
    public event Action<Direction> DownButtonPressed;

    private bool _isJumped;
    private bool _isGamePaused;
    private void Start()
    {
        _isGamePaused = false;
        _isJumped = false;
    }

    private void Update()
    {
        if (!_isGamePaused)
            ProcessTouch();
    }

    private void ProcessTouch()
    {
        if (Input.touchCount > 0 && !_isJumped)
        {
            var touch = Input.GetTouch(0);
            var position = Camera.main.ScreenToViewportPoint(touch.position);

            if (position.y > 0.5f)
            {
                MoveUp();
                _isJumped = true;
            }

            if (position.y < 0.5f)
            {
                MoveDown();
                _isJumped = true;
            }
        }
        if (Input.touchCount == 0)
            _isJumped = false;
    }
    public void TogglePause()
    {
        _isGamePaused = !_isGamePaused;
    }
    private void MoveUp() => UpButtonPressed(Direction.Up);

    private void MoveDown() => DownButtonPressed(Direction.Down);

}
public enum Direction { Up, Down };

