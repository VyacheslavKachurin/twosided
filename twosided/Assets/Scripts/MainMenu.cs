using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private Button _sfxButton;
    [SerializeField] private Button _musicButton;
    private void Start()
    {
        _playButton.onClick.AddListener(PlayGame);
        _highScoreText.text = GameSettings.HighScore.ToString();

        _musicButton.onClick.AddListener(ToggleMusic);
        _sfxButton.onClick.AddListener(ToggleSFX);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ToggleSFX()
    {
        GameSettings.IsSFXOn = !GameSettings.IsSFXOn;
    }
    private void ToggleMusic()
    {
        GameSettings.IsMusicOn = !GameSettings.IsMusicOn;
    }
}
