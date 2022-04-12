using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModel
{
    private UIView _view;
    private int _currentHealth;
    public UIModel(UIView view)
    {
        _currentHealth = 3;
        _view = view;
        _view.Initialize(_currentHealth);
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
}
