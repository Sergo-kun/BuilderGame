using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{

    [SerializeField] Button infoButton;
    [SerializeField] Button meriaHubButton;

    public static event EventHandler OnOpenMeria;

    private int passedId;
    private void Awake() {
        if (infoButton) {
            infoButton.onClick.AddListener(() => {
                BuildingMenuUI.Instance.OpenBuildingMenu(passedId);
                SoundComander.Instance.PlayBip();

                delegat();
            });

            if (meriaHubButton) {
                meriaHubButton.onClick.AddListener(() => {
                    //  OnOpenMeria?.Invoke(this, EventArgs.Empty);
                    MeriaHubUI.Instance.OpenMeria();
                    SoundComander.Instance.PlayBip();

                    delegat();
                });
            }
        }
    }
    private Action delegat;
    public void ShowInfo(int id, Action delegat) {
       this.delegat = delegat;
        if (meriaHubButton) {
            meriaHubButton.gameObject.SetActive(true);
        } else {
            passedId = id;
            infoButton.gameObject.SetActive(true);
        }
        }


    public void HideInfo() {
        infoButton.gameObject.SetActive(false);
        if (meriaHubButton) {
            meriaHubButton.gameObject.SetActive(false);
        }
    }

}
