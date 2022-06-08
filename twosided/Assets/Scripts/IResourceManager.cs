using UnityEngine; 
public interface IResourceManager
{
    public GameObject CreatePrefabInstance(EObject prefab);

    public AudioClip LoadSFX(EAudio audio);
    public AudioSource GetMusic();

}