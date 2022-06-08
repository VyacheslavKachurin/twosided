using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private Transform _player;
    private Transform _platformHolder;

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
        _resourceManager = CompositionRoot.GetResourceManager();

        _platformHolder = _resourceManager.CreatePrefabInstance(EObject.PlatformHolder).transform;
        _platformHolder.transform.position = Vector3.zero;

        for (int i = 0; i < 5; i++)
        {
            AddPlatform();
        }
    }
    private void AddPlatform()
    {
        if (_isGameFinished)
            return;

        var dividerInstance = _resourceManager.CreatePrefabInstance(EObject.Platform);
        dividerInstance.transform.position = _spawnPoint;
        dividerInstance.transform.SetParent(_platformHolder);
        dividerInstance.GetComponent<ObjectSpawner>().InstanceDestroyed += AddPlatform;
        _spawnPoint += new Vector2(dividerInstance.transform.localScale.x, 0);

        _dividers.Enqueue(dividerInstance.transform);
    }

    public void StopSpawning()
    {
        _isGameFinished = true;
    }

}
