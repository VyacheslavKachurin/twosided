using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _divider;
    [SerializeField] private InputController _inputController;
    [SerializeField] private CameraFollow _camera;

    private PlayerController _playerController;

    private Vector3 playerSpawnPoint = new Vector3(-5.5f, 3.3f, 0);

    private void Start()
    {
        _player=Instantiate(_player,playerSpawnPoint,Quaternion.identity);
        _playerController = _player.GetComponent<PlayerController>();
        _playerController.Initialize();

 

        _inputController.UpButtonPressed += _playerController.Move;
        _inputController.DownButtonPressed += _playerController.Move;

        _camera = Instantiate(_camera);
        _camera.SetPlayer(_player);
    }
}
