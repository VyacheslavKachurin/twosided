using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private float _offsetX;
    private void Start()
    {
        _offsetX = _player.position.x - transform.position.x;
    }

    private void Update()
    {
        transform.position = new Vector2(_player.position.x + _offsetX, 0);
    }
    public void SetPlayer(GameObject player)
    {
        _player = player.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
