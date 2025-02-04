using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] private AudioClip equippedMusicClip;

    [SerializeField] private OptionsSO optionsSettings; 
    public static MusicManager Instance { get; private set; }


    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        if (Instance == null) {
            Instance = this;
        }

    }

    void Start() {
        EquipedMusicPlayer();
    }



    public void EquipedMusicPlayer() {
        audioSource.clip = equippedMusicClip;
        audioSource.volume = optionsSettings.musicVolume;
        audioSource.Play();
    }

}
