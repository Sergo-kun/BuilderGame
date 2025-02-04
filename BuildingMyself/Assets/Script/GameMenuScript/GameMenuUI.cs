using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    private void Awake() {
        if (playButton) {
            playButton.onClick.AddListener(() => {
                SoundComander.Instance.PlayBip();
                ScenesLoader.Load(ScenesLoader.GameScene.Game);
            });
        }
        if (optionsButton) {
            optionsButton.onClick.AddListener(() => {
                SoundComander.Instance.PlayBip();

                ScenesLoader.Load(ScenesLoader.GameScene.Options);
            });
        }

        if (exitButton) {
            exitButton.onClick.AddListener(() => {
                SoundComander.Instance.PlayBip();

                Application.Quit();
            });
        }
    }
}
