using UnityEngine;
using UnityEngine.UI;

public class BuildingPlaceUI : MonoBehaviour {
    [SerializeField] GameObject placeButtons;
    [SerializeField] Button placeButton;
    [SerializeField] Button cancelButton;

    private void Awake() {
        placeButtons.SetActive(false);

        if (placeButton) {
            placeButton.onClick.AddListener(() => {
                GridSystem.Instance.PlaceObject();
                GameManager.Instance.SwichToMoveRegime();
                SoundComander.Instance.PlayBuilding();

                BuildingsSelectionHolderUI.Instance.UntogleBordersUI();
            });
        }

        if (cancelButton) {
            cancelButton.onClick.AddListener(() => {
                GameManager.Instance.SwichToMoveRegime();
                SoundComander.Instance.PlayBip();

                BuildingsSelectionHolderUI.Instance.UntogleBordersUI();
            });
        }
    }

    public void ShowHideButtons(bool whatToDo) {
        placeButtons.SetActive(whatToDo);
    }
}
