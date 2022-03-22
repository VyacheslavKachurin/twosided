using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleGeneration : MonoBehaviour
{
    public event Action InstanceDestroyed; 

    //this script is responsible for creating obstacles on the platform
    public void Start()
    {
    }
    private void OnDestroy()
    {
        InstanceDestroyed?.Invoke();
    }

}
