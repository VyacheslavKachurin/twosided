using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModel
{
    private UIView _view;
    private int _currentHealth;

    private int _coinScore = 10;
    private int _distanceScore = 1;
    private float _fullness;
    public UIModel(UIView view, int maxHealth, GameManager _gameManager, Player _player)
    {
        _currentHealth = maxHealth;
        _view = view;
        _view.Initialize(_currentHealth);
        _view.PauseClicked += TogglePauseMenu;
        _view.PauseClicked += _gameManager.TogglePause;
        _view.MenuClicked += _gameManager.LoadMenu;
        _view.TopButton.ButtonPressed += _player.Move;
        _view.DownButton.ButtonPressed += _player.Move;
        _view.RestartClicked += _gameManager.RestartLevel;
        _player.PlayerMoved += AddDistanceScore;
        _player.PlayerMoved += DecreaseFullness;
        _player.CoinPickedUp += AddCoinScore;
        _player.CoinPickedUp += AddFullness;
    }

    private void AddCoinScore()
    {
        _view.UpdateScore(_coinScore);
    }

    private void AddDistanceScore()
    {
        _view.UpdateScore(_distanceScore);
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

    private void AddFullness()
    {
        _fullness += 0.07f;
        _view.UpdateBelly(_fullness);
    }

    private void DecreaseFullness()
    {
        _fullness -= 0.01f;
        _view.UpdateBelly(_fullness);
    }
}
