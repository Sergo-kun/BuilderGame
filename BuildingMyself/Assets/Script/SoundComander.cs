using UnityEngine;

public class SoundComander : MonoBehaviour
{
    [SerializeField] private AudioClip bipSfx;
    [SerializeField] private AudioClip byeSfx;
    [SerializeField] private AudioClip buildSfx;


    public static SoundComander Instance { get; private set; }
    private void Awake() {
        Instance = this;
    }
    public void PlayBip() {
        SoundManager.Instance.PlayAudioClip(bipSfx);
    } 
    public void PlayBuy() {
        SoundManager.Instance.PlayAudioClip(byeSfx);
    }
    
    public void PlayBuilding() {
        SoundManager.Instance.PlayAudioClip(buildSfx, 5);
    }
}
