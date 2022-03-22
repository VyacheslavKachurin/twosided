using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _dividerPrefab;

    private Vector2 _spawnPoint = new Vector2(0, 0);

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {

    }

    private void Initialize()
    {
        for (int i = 0; i < 5; i++)
        {
            AddDivider();
        }
    }
    private void AddDivider()
    {
        var dividerInstance = Instantiate(_dividerPrefab, _spawnPoint, Quaternion.identity);
        dividerInstance.GetComponent<ObstacleGeneration>().InstanceDestroyed += AddDivider;
        _spawnPoint += new Vector2(_dividerPrefab.transform.localScale.x, 0);
    }
}
