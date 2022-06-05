using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModel
{
    private UIView _view;
    private int _currentHealth;
    public UIModel(UIView view, int maxHealth, GameManager _gameManager, Player _playerController)
    {
        _currentHealth = maxHealth;
        _view = view;
        _view.Initialize(_currentHealth);
        _view.PauseClicked += TogglePauseMenu;
        _view.PauseClicked += _gameManager.TogglePause;
        _view.MenuClicked += _gameManager.LoadMenu;
        _view.TopButton.ButtonPressed += _playerController.Move;
        _view.DownButton.ButtonPressed += _playerController.Move;
        _view.RestartClicked += _gameManager.RestartLevel;
    }

    public void UpdateHealth(int newHealth)
    {
        if (newHealth > _currentHealth)
        {
            _view.FillHeart();
            _currentHealth = newHealth;
        }
        else
        {
            _view.EmptyHeart();
            _currentHealth = newHealth;
        }
    }

    private void TogglePauseMenu()
    {
        _view.TogglePauseMenu();
    }

    public void ToggleGameOverMenu()
    {
        _view.ToggleGameOverMenu();

    }
}
