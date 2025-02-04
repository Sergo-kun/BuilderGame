using System;
using UnityEngine;
using UnityEngine.UI;

public class StoreCardBuildingUI : MonoBehaviour
{
    [SerializeField] private BuildingsSO buildindsSO;


    [SerializeField] private GameObject selectedBorder;
    [SerializeField] private GameObject boughtMark;
    private Button button;

    public class OnSellectButtonEventArgs : EventArgs {
       public BuildingsSO buildindsSO;
        public Action delegat;

    }

    public static event EventHandler<OnSellectButtonEventArgs> OnSellectButton;

    public void CheckIsBought() {
        if (WeGiveOutThings.Instance.GetCityStatusSO().CheckIsBought(buildindsSO.currentBuilding)) {
            boughtMark.SetActive(true);
        } else {
            boughtMark.SetActive(false);
        }

    }

    private void OnEnable() {

        CheckIsBought();
    }

    private void Awake() {

        if (selectedBorder != null) {
            selectedBorder.SetActive(false);
        }
        button = GetComponent<Button>();
        if (button) {
            button.onClick.AddListener(() => {
                SoundComander.Instance.PlayBip();

                PressingButton();
                
            });
        }
    }

    public void PressingButton() {
        // set data on the screan
        OnSellectButton?.Invoke(this, new OnSellectButtonEventArgs {
            buildindsSO = buildindsSO,
            delegat = CheckIsBought
        });
        SellectBorder();
    }
    public void UnselectBorder() {
        if (selectedBorder != null) {
            selectedBorder.SetActive(false);
        }
        if (button) {

        button.enabled = true;
        }
    }

    public void SellectBorder() {
        if (selectedBorder != null) {
            selectedBorder.SetActive(true);
        }
        if (button) {
            button.enabled = false;
        }
    }
 
}
