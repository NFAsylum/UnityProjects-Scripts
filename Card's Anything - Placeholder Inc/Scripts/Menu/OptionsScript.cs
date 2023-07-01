using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsScript : MonoBehaviour
{
    bool _canSet = true, _isMuted;

    [SerializeField]
    AudioMixer _audio;


    public void SetFullScreen(bool isFullScreen)
    {
        if (_canSet)
        {
            _canSet = false;

            StartCoroutine(SetFullScreenCoroutine(isFullScreen));
        }
    }

    IEnumerator SetFullScreenCoroutine(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        print("Game set to fullscreen: " + isFullScreen);
        //print("Is FullScreen: " + isFullScreen);

        yield return new WaitForSeconds(0.05f);

        _canSet = true;
    }

    public void SetVolume(float volume)
    {
        if (_canSet)
        {
            _canSet = false;

            StartCoroutine(SetVolumeCoroutine(volume));
        }
    }

    IEnumerator SetVolumeCoroutine(float volume)
    {
        _audio.SetFloat("Volume", volume);

        print("Game set to volume: " + volume);

        yield return new WaitForSeconds(0.05f);

        _canSet = true;
    }
}
