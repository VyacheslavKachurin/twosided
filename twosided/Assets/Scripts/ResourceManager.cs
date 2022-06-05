using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : IResourceManager
{
    private const string _prefabsPath = "Prefabs/";
    public GameObject CreatePrefabInstance(EObjects prefab)
    {
        var obj = GameObject.Instantiate(Resources.Load<GameObject>(_prefabsPath + prefab.ToString()));
        return obj;
    }
}

public enum EObjects { Platform, PlatformHolder, Player,Camera };
