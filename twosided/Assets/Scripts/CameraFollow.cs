using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _player;
    private float _offset;
    public void SetPlayer(GameObject player)
    {
        transform.position = new Vector3(0, 0, -10);

        _player = player.transform;
        _offset = transform.position.x - _player.position.x;


    }
    private void LateUpdate()
    {
        transform.position = new Vector3(_player.position.x, transform.position.y, transform.position.z);
    }
}
