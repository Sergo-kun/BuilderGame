using System.Collections.Generic;
using UnityEngine;

public class StoreCardHolderUI : MonoBehaviour
{
    [SerializeField] private List<StoreCardBuildingUI> cardHolderList = new List<StoreCardBuildingUI>();

    
    private void Awake() {
        StoreCardBuildingUI.OnSellectButton += StoreCardBuildingUI_OnSellectButton;
    }

    private void OnDestroy() {
        StoreCardBuildingUI.OnSellectButton -= StoreCardBuildingUI_OnSellectButton;
    }

    private void OnEnable() {
        cardHolderList[0].PressingButton();
    }

    private void StoreCardBuildingUI_OnSellectButton(object sender, StoreCardBuildingUI.OnSellectButtonEventArgs e) {
        foreach (StoreCardBuildingUI holder in cardHolderList) {
            holder.UnselectBorder();
        }
    }

    /*public void UnselectCards() {
        foreach (StoreCardBuildingUI holder in cardHolderList) {
            holder.UnselectBorder();
        }
    }*/
}
