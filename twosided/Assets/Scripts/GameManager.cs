using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _divider;
    [SerializeField] private InputController _inputController;
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private Cleaner _cleaner;

    private PlayerController _playerController;

    private Vector2 _cleanerSpawnPoint;
    private Vector3 playerSpawnPoint = new Vector3(-5.5f, 3.3f, 0);

    private void Start()
    {
        _cleanerSpawnPoint = new Vector2(_divider.transform.localScale.x*2,0);

        _player=Instantiate(_player,playerSpawnPoint,Quaternion.identity);
        _playerController = _player.GetComponent<PlayerController>();
        _playerController.Initialize(); 

        _inputController.UpButtonPressed += _playerController.Move;
        _inputController.DownButtonPressed += _playerController.Move;

        _camera = Instantiate(_camera);
        _camera.SetPlayer(_player);

        _levelGenerator = Instantiate(_levelGenerator);

        _cleaner = Instantiate(_cleaner, _cleanerSpawnPoint, Quaternion.identity);
        _cleaner.SetPlayer(_player);
    }
}
