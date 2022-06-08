using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIModel
{
    public Action PlayerStarved;
    private UIView _view;
    private int _currentHealth;

    private int _coinScore = 10;
    private int _distanceScore = 1;
    private int _totalScore;
    private float _fullness;

    private float FullnessProperty
    {
        get { return _fullness; }
        set
        {
            if (value <= 1f)
            {
                _fullness = value;
                if (_fullness <= 0)
                {
                    PlayerStarved?.Invoke();
                }
            }
        }
    }

    public UIModel(UIView view, int maxHealth, GameManager _gameManager, Player _player)
    {
        _totalScore = 0;
        FullnessProperty = 1f;
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

    public void SaveScore()
    {
        Debug.Log("saving score");
        GameSettings.HighScore =_totalScore;
    }

    private void AddCoinScore()
    {
        _totalScore += _coinScore;
        _view.UpdateScore(_coinScore);
    }

    private void AddDistanceScore()
    {
        _totalScore += _distanceScore;
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
        FullnessProperty += 0.07f;
        _view.UpdateBelly(_fullness);
    }

    private void DecreaseFullness()
    {
        FullnessProperty -= 0.01f;
        _view.UpdateBelly(_fullness);
    }
}
