using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : IResourceManager
{
    private const string _prefabsPath = "Prefabs/";
    private const string _sfxPath = "SFX/";
    private const string _musicPath = "Music/";
    public GameObject CreatePrefabInstance(EObject prefab)
    {
        var obj = GameObject.Instantiate(Resources.Load<GameObject>(_prefabsPath + prefab.ToString()));
        return obj;
    }

    public AudioClip LoadSFX(EAudio audio)
    {
        var obj = Resources.Load<AudioClip>(_sfxPath + audio.ToString());
        return obj;
    }

    public AudioSource GetMusic()
    {
        var obj = GameObject.Instantiate<AudioSource>(Resources.Load<AudioSource>(_musicPath + "MainTheme"));
        return obj;
    }

}

public enum EObject { Platform, PlatformHolder, Player, Camera, Coin, Obstacle, Heart };
public enum EAudio {Jump,Switch,Landing,Hit,Collectible,Theme}
