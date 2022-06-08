using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    private IResourceManager _resourceManager;
    private Transform _playerTransform;
    private Dictionary<EAudio, AudioClip> _sounds = new Dictionary<EAudio, AudioClip>();

   // private bool _isMusicOn;
    //private bool _isSFXOn;
    public AudioManager()
    {
        _resourceManager = CompositionRoot.GetResourceManager();
        _playerTransform = CompositionRoot.GetPlayer().transform;
        PlayTheme();

      //  _isMusicOn = GameSettings.IsMusicOn;
       // _isSFXOn = GameSettings.IsSoundEffectsOn;
    }

    public void PlaySound(EAudio audioclip)
    {
        if (!IsSFXOn())
            return;

        if (!_sounds.ContainsKey(audioclip))
        {
            var sound = _resourceManager.LoadSFX(audioclip);
            _sounds[audioclip] = sound;
        }

        AudioSource.PlayClipAtPoint(_sounds[audioclip], _playerTransform.transform.position);
    }

    private void PlayTheme()
    {
        if (!IsMusicOn())
            return;
        var theme = _resourceManager.GetMusic();

    }

    private bool IsMusicOn() => GameSettings.IsMusicOn;

    private bool IsSFXOn() => GameSettings.IsSFXOn;

}
