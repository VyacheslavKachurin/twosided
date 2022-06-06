using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public event Action InstanceDestroyed;
    private List<Vector2> _spawnedObjects = new List<Vector2>();
    private float _obstacleGap = 1.1f;
    private IResourceManager _resourceManager;

    private const float _distanceUnit = 0.5f;


    public void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _resourceManager = CompositionRoot.GetResourceManager();

        var randomObjectAmount = UnityEngine.Random.Range(1, 5);
        for (int i = 0; i < randomObjectAmount; i++)
        {
            SpawnEtype(EObjects.Obstacle);
        }

        for (int i = 0; i < 4; i++)
        {
            SpawnEtype(EObjects.Coin);
        }

        int random = UnityEngine.Random.Range(0, 10);
        if (random > 8)
            SpawnEtype(EObjects.Heart);
    }

    private void OnDestroy()
    {
        InstanceDestroyed?.Invoke();
    }

    private void SpawnEtype(EObjects etype)
    {
        var etypeInstance = _resourceManager.CreatePrefabInstance(etype);

        var etypeSpawnPoint = CalculateRandomPosition(etype);
        etypeInstance.transform.position = (Vector2)transform.position + etypeSpawnPoint;
        etypeInstance.transform.SetParent(transform);
    }

    private Vector2 CalculateRandomPosition(EObjects Etype)
    {
        int sign = UnityEngine.Random.Range(0, 2) * 2 - 1;

        float yPosition = sign * (_distanceUnit + transform.localScale.y / 2);
        int xPosition = (int)UnityEngine.Random.Range((-transform.localScale.x / 2) + _distanceUnit, (transform.localScale.x / 2) - _distanceUnit);

        Vector2 spawnPosition = new Vector2(xPosition, yPosition);

        var result = CheckForDistance(spawnPosition);

        if (result)
        {
            _spawnedObjects.Add(spawnPosition);
            return spawnPosition;
        }
        else if (Etype != EObjects.Obstacle)
        {
            if (sign > 0) spawnPosition.y += _distanceUnit * 4;
            else spawnPosition.y -= _distanceUnit * 4;
            return spawnPosition;
        }
        else
            return CalculateRandomPosition(Etype);
    }

    private bool CheckForDistance(Vector2 offset)
    {
        foreach (var position in _spawnedObjects)
        {
            if (position.y == offset.y)
            {
                if (Vector2.Distance(position, offset) < _obstacleGap)
                    return false;
            }
        }
        return true;
    }


}
