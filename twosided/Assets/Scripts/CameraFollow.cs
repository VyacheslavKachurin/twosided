using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _player;
    private float _offset;
    private Vector3 _velocity = Vector3.zero;
    private float _smoothTime = 0.01f;
    public void SetPlayer(GameObject player)
    {
        transform.position = new Vector3(0, 0, -10);

        _player = player.transform;
        _offset = transform.position.x - _player.position.x;


    }
    private void LateUpdate()
    {         
        Vector3 targetPosition= new Vector3(_player.position.x+_offset, transform.position.y, transform.position.z);
        //transform.position = targetPosition;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.9f);
    }
}
