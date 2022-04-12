using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{
    public event Action InstanceDestroyed;

    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private GameObject _heartPrefab;
    private const float _heartSpawnChance = 0.9f;
    private List<Vector2> _obstacles = new List<Vector2>();
    private float _obstacleGap = 1.1f;

    public void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        var randomObjectAmount = UnityEngine.Random.Range(1, 5);
        for (int i = 0; i < randomObjectAmount; i++)
        {
            var randomPrefab = ChooseObject();
            SpawnObject(randomPrefab);
        }
    }

    private void OnDestroy()
    {
        InstanceDestroyed?.Invoke();
    }

    private void SpawnObject(GameObject randomPrefab)
    {
        var objectSpawnPoint = CalculateRandomPosition();
        var objectInstance = Instantiate(randomPrefab, (Vector2)transform.position + objectSpawnPoint, Quaternion.identity);
        objectInstance.transform.SetParent(transform);
    }

    private Vector2 CalculateRandomPosition()
    {
        int sign = UnityEngine.Random.Range(0, 2) * 2 - 1;
        float yPosition = sign * (_obstaclePrefab.transform.localScale.y / 2 + transform.localScale.y / 2);
        float xPosition = UnityEngine.Random.Range((-transform.localScale.x / 2) + _obstaclePrefab.transform.localScale.x / 2, (transform.localScale.x / 2) - _obstaclePrefab.transform.localScale.x / 2);

        Vector2 offset = new Vector2(xPosition, yPosition);

        var result = CheckForDistance(offset);

        if (result)
        {
            _obstacles.Add(offset);
            return offset;
        }
        else
            return CalculateRandomPosition();
    }

    private bool CheckForDistance(Vector2 offset)
    {
        foreach (var position in _obstacles)
        {
            if (position.y == offset.y)
            {
                if (Vector2.Distance(position, offset) < _obstacleGap)
                    return false;
            }
        }
        return true;
    }

    private GameObject ChooseObject()
    {
        if (UnityEngine.Random.value > _heartSpawnChance)
            return _heartPrefab;
        else
            return _obstaclePrefab;
    }
}
