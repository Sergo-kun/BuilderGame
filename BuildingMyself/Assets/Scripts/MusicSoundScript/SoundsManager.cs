using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private OptionsSO settingsData;

    public static SoundManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public void PlayAudioClip(AudioClip audioClip, float volumeMod = 1f) {
     
        GameObject temporaryAudioObj = new GameObject("TemporarySound");

       
        AudioSource audioSource = temporaryAudioObj.AddComponent<AudioSource>();

        
        float baseVolume = settingsData.sfxVolume;
        audioSource.volume = Mathf.Clamp(baseVolume * volumeMod, 0f, 1f);

       
        audioSource.PlayOneShot(audioClip);

       
        Destroy(temporaryAudioObj, audioClip.length + 1f);
    }

    internal void PlayAudioClip() {
        throw new NotImplementedException();
    }
}
