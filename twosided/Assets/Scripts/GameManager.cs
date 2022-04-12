using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _divider;
    [SerializeField] private InputController _inputController;
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private UIView _UIView;

    [SerializeField] private LevelGenerator _levelGenerator;
    private UIModel _UIModel;
    private PlayerController _playerController;

    private Vector3 playerSpawnPoint = new Vector3(-5.5f, 3.3f, 0);

    private void Start()
    {
        _UIView = Instantiate(_UIView);
        _UIModel = new UIModel(_UIView);

        _player=Instantiate(_player,playerSpawnPoint,Quaternion.identity);
        _playerController = _player.GetComponent<PlayerController>();
        _playerController.Initialize();
        _playerController.HealthChanged += _UIModel.UpdateHealth;
        _playerController.PlayerDied += _inputController.TogglePause;

        _inputController.UpButtonPressed += _playerController.Move;
        _inputController.DownButtonPressed += _playerController.Move;

        _camera = Instantiate(_camera);
        _camera.SetPlayer(_player);

        _levelGenerator = Instantiate(_levelGenerator);
        _levelGenerator.SetPlayer(_player.transform);

    }
}
