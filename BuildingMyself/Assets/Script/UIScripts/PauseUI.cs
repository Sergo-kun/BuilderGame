using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToggle;

    [SerializeField] Button pauseButton;
    [SerializeField] GameObject pauseCollum;

    [SerializeField] Button resumeButton;
    [SerializeField] Button menuButton;

    private void Awake() {
        if (pauseButton) {
            pauseButton.onClick.AddListener(() => {
                SetPause();
                SoundComander.Instance.PlayBip();

            });
        }

        if (resumeButton) {
            resumeButton.onClick.AddListener(() => {
                SetResume();
                SoundComander.Instance.PlayBip();

            });
        }

        if (menuButton) {
            menuButton.onClick.AddListener(() => {
                SoundComander.Instance.PlayBip();

                ScenesLoader.Load(ScenesLoader.GameScene.Menu);
            });
        }
    }

    private void SetPause() {
        GameManager.Instance.SetOpenMenuRegime();
        pauseCollum.SetActive(true);
        HideObjects();
    }

    private void SetResume() {
        GameManager.Instance.CancelOpenMenuRegime();
        pauseCollum.SetActive(false);
        ShowObjects();
    }

    private List<GameObject> objectsToShow;
    private void HideObjects() {
        objectsToShow = new List<GameObject>();
        foreach (GameObject obj in objectsToggle) {
            if (obj.activeSelf) {
                objectsToShow.Add(obj);
                obj.SetActive(false);
            }
        }

    }

    private void ShowObjects() {
        foreach (GameObject obj in objectsToShow) {
            obj.SetActive(true);
        }
    }
}
