using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UIView : MonoBehaviour
{
    public event Action PauseClicked;
    public event Action MenuClicked;
    public event Action RestartClicked;

    public ActionButton TopButton;
    public ActionButton DownButton;

    [SerializeField] private Transform _healthParent;
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Sprite _filledHeart; //TODO: assign sprites instead of colors;
    [SerializeField] private Sprite _emptyHeartSprite;

    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Image _hungerImage;

    private List<Image> _hearts = new List<Image>();
    private int _currentScore = 0;

    public void Initialize(int heartsAmount)
    {
        UpdateScore(0);
        UpdateBelly(1f);

        for (int i = 0; i < heartsAmount; i++)
        {
            var heartInstance = Instantiate(_heartPrefab, _healthParent).GetComponent<Image>();
            _hearts.Add(heartInstance);
            FillHeart();
        }
        _pauseButton.onClick.AddListener(ClickPause);

        _continueButton.onClick.AddListener(ClickPause);
        _mainMenuButton.onClick.AddListener(ClickMenu);
        _restartButton.onClick.AddListener(ClickRestart);
    }

    private void ClickPause()
    {
        PauseClicked?.Invoke();
    }

    private void ClickRestart()
    {
        RestartClicked?.Invoke();
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

    public void ToggleGameOverMenu()
    {
        TogglePauseMenu();
        _restartButton.gameObject.SetActive(true);
        _continueButton.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        _currentScore += score;
        _scoreText.text = _currentScore.ToString();
    }

    public void UpdateBelly(float value)
    {
        _hungerImage.fillAmount = value;
    }
}
