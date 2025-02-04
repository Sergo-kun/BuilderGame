using UnityEngine;
using UnityEngine.UI;

public class BuildingsScrollUI : MonoBehaviour
{
    [SerializeField] private Button hideButton;
    [SerializeField] private Button showButton;

    [SerializeField] private GameObject scrolBuildings;

    public static BuildingsScrollUI Instance { get; private set; }

    

    private void Awake() {
        Instance = this;
        if (hideButton) {
            hideButton.onClick.AddListener(() => {
                SoundComander.Instance.PlayBip();
                HideScroll();
            });
        } 
        if (showButton) {
            showButton.onClick.AddListener(() => {
                SoundComander.Instance.PlayBip();
                ShowScroll();
            });
        }
    }

    private void Start() {
        HideScroll();
    }

    private void ShowScroll() {
        scrolBuildings.SetActive(true);
        showButton.gameObject.SetActive(false);
    }

    public void HideScroll() {
        scrolBuildings.SetActive(false);
        showButton.gameObject.SetActive(true);
    }
}
