using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //[SerializeField] private GameObject _platformPrefab;
    [SerializeField] private Transform _floorHolder;
    [SerializeField] private Transform _player;

    private Queue<Transform> _dividers = new Queue<Transform>();
    private Transform _lastDivider;

    private Vector2 _spawnPoint = new Vector2(0, 0);
    private bool _isGameFinished = false;

    private IResourceManager _resourceManager;
    private void Start()
    {
        Initialize();


    }

    private void Update()
    {
        if (_dividers.Count > 0)
            CheckForPlayer();
    }

    public void SetPlayer(Transform player)
    {
        _player = player;
    }

    private void CheckForPlayer()
    {
        if (_lastDivider == null)
        {
            _lastDivider = _dividers.Dequeue();
        }

        if (_lastDivider.position.x + _lastDivider.localScale.x < _player.position.x)
        {
            Destroy(_lastDivider.gameObject);
            _lastDivider = null;
        }
    }

    private void Initialize()
    {
        Debug.Log("Initialized");
        _floorHolder = Instantiate(_floorHolder, Vector3.zero, Quaternion.identity);
        for (int i = 0; i < 5; i++)
        {
            AddDivider();
        }
    }
    private void AddDivider()
    {
        if (_isGameFinished)
            return;

        _resourceManager = new ResourceManager();
        var dividerInstance = _resourceManager.CreatePrefabInstance(EObjects.Platform);
        dividerInstance.transform.position = _spawnPoint;
        dividerInstance.transform.SetParent(_floorHolder);
        dividerInstance.GetComponent<ObstacleSpawner>().InstanceDestroyed += AddDivider;
        _spawnPoint += new Vector2(dividerInstance.transform.localScale.x, 0);

        _dividers.Enqueue(dividerInstance.transform);
    }

    public void StopSpawning()
    {
        _isGameFinished = true;
    }

}
