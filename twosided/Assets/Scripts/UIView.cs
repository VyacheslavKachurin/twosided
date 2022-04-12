using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIView : MonoBehaviour
{
    public event Action PauseClicked;
    public event Action MenuClicked;

    public ActionButton TopButton;
    public ActionButton DownButton;

    [SerializeField] private Transform _healthParent;
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Sprite _filledHeart; //TODO: assign sprites instead of colors;
    [SerializeField] private Sprite _emptyHeartSprite;

    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _mainMenuButton;



    [SerializeField] private GameObject _pauseMenu;

    private List<Image> _hearts = new List<Image>();

    public void Initialize(int heartsAmount)
    {
        for (int i = 0; i < heartsAmount; i++)
        {
            var heartInstance = Instantiate(_heartPrefab, _healthParent).GetComponent<Image>();
            _hearts.Add(heartInstance);
            FillHeart();
        }
        _pauseButton.onClick.AddListener(ClickPause);

        _continueButton.onClick.AddListener(ClickPause);
        _mainMenuButton.onClick.AddListener(ClickMenu);
    }

    private void ClickPause()
    {
        PauseClicked?.Invoke();
    }

    private void ClickMenu()
    {
        MenuClicked?.Invoke();
    }

    public void FillHeart()
    {
        foreach (var heart in _hearts)
        {
            if (heart.color == Color.white)
            {
                heart.color = Color.red;
                return;
            }
        }
    }

    public void EmptyHeart()
    {
        var reversedHearts = new List<Image>(_hearts);
        reversedHearts.Reverse();
        foreach (var heart in reversedHearts)
        {
            if (heart.color == Color.red)
            {
                heart.color = Color.white;
                return;
            }
        }
    }

    public void TogglePauseMenu()
    {
        _pauseMenu.SetActive(!_pauseMenu.activeInHierarchy);
    }
}
