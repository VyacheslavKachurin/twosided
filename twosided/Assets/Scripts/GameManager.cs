using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class GameManager : MonoBehaviour
{
    [SerializeField] private UIView _UIView;

    [SerializeField] private LevelGenerator _levelGenerator;

    private CameraFollow _camera;
    private UIModel _UIModel;
    private Player _player;

    private Vector3 _playerSpawnPoint = new Vector3(-5.5f, 3.3f, 0);

    private void Start()
    {
        Time.timeScale = 1;
        _UIView = Instantiate(_UIView);

        _player = CompositionRoot.GetPlayer();
        _player.transform.position = _playerSpawnPoint;
        _player = _player.GetComponent<Player>();
        _player.Initialize();
        _player.PlayerDied += ShowGameOver;
        _player.PlayerDied += _levelGenerator.StopSpawning;

        _UIModel = new UIModel(_UIView, _player.MaxHealth, this, _player);
        _UIModel.PlayerStarved += ShowGameOver;

        _player.HealthChanged += _UIModel.UpdateHealth;

        _camera = CompositionRoot.GetCamera();
        _camera.SetPlayer(_player.gameObject);

        _levelGenerator = Instantiate(_levelGenerator);
        _levelGenerator.SetPlayer(_player.transform);

#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += PlayModeChange;
#endif

    }

    public void TogglePause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void LoadMenu()
    {
        _levelGenerator.StopSpawning();
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        _levelGenerator.StopSpawning();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ShowGameOver()
    {
        _UIModel.SaveScore();
        TogglePause();
        _levelGenerator.StopSpawning();
        _UIModel.ToggleGameOverMenu();
    }

#if UNITY_EDITOR
    private void PlayModeChange(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            _levelGenerator.StopSpawning();
        }
    }
#endif
}
