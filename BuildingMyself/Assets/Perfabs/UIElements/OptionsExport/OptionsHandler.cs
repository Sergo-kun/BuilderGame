using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour
{
    [SerializeField] Button buttonBack;
    [SerializeField] AudioClip clipSfx;

    [SerializeField] Slider clipSfxSlider;
    [SerializeField] TextMeshProUGUI sfxInfoNum;
    [SerializeField] AudioSource sourceAudio;
    
    
    [SerializeField] Slider sliderSong;
    [SerializeField] TextMeshProUGUI songInfoNum;
    [SerializeField] AudioSource sourceSong;
    [SerializeField] OptionsSO settingsFile;

    [SerializeField] Button deleteProgressButton;

    private float audioLingering = 0.5f; // Cooldown time in seconds
    private float lastAudioTime;
    private void Awake() {
        InitializeAudioSettings();
        AddButtonListeners();
        AddSliderListeners();
    }

    private void InitializeAudioSettings() {
        sourceSong.volume = settingsFile.musicVolume;
        sourceAudio.volume = settingsFile.sfxVolume;

        clipSfxSlider.value = settingsFile.sfxVolume;
        sliderSong.value = settingsFile.musicVolume;

        sfxInfoNum.text = ((int)(clipSfxSlider.value * 100)).ToString();
        songInfoNum.text = ((int)(sliderSong.value * 100)).ToString();
    }

    private void AddButtonListeners() {
        buttonBack.onClick.AddListener(() => {
            ScenesLoader.Load(ScenesLoader.GameScene.Menu);
        });

        if (deleteProgressButton) {
            deleteProgressButton.onClick.AddListener(() => {
                SavingDuringGame.Instance.deleteProgress();
            });
        }
    }

    private void AddSliderListeners() {
        clipSfxSlider.onValueChanged.AddListener(value => {
            UpdateSfxVolume(value);
        });

        sliderSong.onValueChanged.AddListener(value => {
            UpdateSongVolume(value);
        });
    }

    private void UpdateSfxVolume(float value) {
        sfxInfoNum.text = ((int)(value * 100)).ToString();
        settingsFile.sfxVolume = value;

        if (Time.time - lastAudioTime >= audioLingering) {
            StartCoroutine(PlayAudio());
            lastAudioTime = Time.time;
        }
    }

    private void UpdateSongVolume(float value) {
        sourceSong.volume = value;
        settingsFile.musicVolume = value;
        songInfoNum.text = ((int)(value * 100)).ToString();
    }

    private IEnumerator PlayAudio() {
        sourceAudio.PlayOneShot(clipSfx, clipSfxSlider.value);
        yield return new WaitForSeconds(clipSfx.length);
    }



}
