using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _dividerPrefab;
    [SerializeField] private Transform _floorHolder;
    [SerializeField] private Transform _player;

    private Queue<Transform> _dividers = new Queue<Transform>();
    private Transform _lastDivider;

    private Vector2 _spawnPoint = new Vector2(0, 0);

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
        _floorHolder = Instantiate(_floorHolder, Vector3.zero, Quaternion.identity);
        for (int i = 0; i < 5; i++)
        {
            AddDivider();
        }
    }
    private void AddDivider()
    {
        var dividerInstance = Instantiate(_dividerPrefab, _spawnPoint, Quaternion.identity);
        dividerInstance.transform.SetParent(_floorHolder);
        dividerInstance.GetComponent<ObstacleGeneration>().InstanceDestroyed += AddDivider;
        _spawnPoint += new Vector2(_dividerPrefab.transform.localScale.x, 0);

        _dividers.Enqueue(dividerInstance.transform);
    }
}
