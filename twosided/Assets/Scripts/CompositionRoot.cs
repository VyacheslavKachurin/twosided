using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositionRoot : MonoBehaviour
{
    private static GameObject _player;
    private static IResourceManager _resourceManager;
    private static CameraFollow _camera;

    public static IResourceManager GetResourceManager()
    {
        if (_resourceManager == null)
            _resourceManager = new ResourceManager();
        return _resourceManager;
    }

    public static Player GetPlayer()
    {
        if (_player == null)
            _player = _resourceManager.CreatePrefabInstance(EObjects.Player);
        return _player.GetComponent<Player>();
    }

    public static CameraFollow GetCamera()
    {
        if (_camera == null)
            _camera = _resourceManager.CreatePrefabInstance(EObjects.Camera).GetComponent<CameraFollow>();
        return _camera;
    }

    private void OnDestroy()
    {
        _player = null;
        _resourceManager = null;
    }
    private void Awake()
    {
        GetResourceManager();
    }
}
