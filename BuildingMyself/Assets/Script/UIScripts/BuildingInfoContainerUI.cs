using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfoContainerUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI moneyBonus;
    [SerializeField] TextMeshProUGUI peopleBonus;
    [SerializeField] TextMeshProUGUI literacyBonus;
    [SerializeField] TextMeshProUGUI moodBonus;
    [SerializeField] TextMeshProUGUI priceToBye;
    [SerializeField] Button byeButton;

    [SerializeField] GameObject purchasedScenario;
    [SerializeField] GameObject byeScenario;

    [SerializeField] Sprite enableSprite;
    [SerializeField] Sprite disableSprite;

    private BuildingsSO buildingsSO;

    private void Awake() {
        if (byeButton) {

            byeButton.onClick.AddListener(() => {
                SoundComander.Instance.PlayBuy();

                Bye();
            });
        }

        StoreCardBuildingUI.OnSellectButton += StoreCardBuildingUI_OnSellectButton;
        SavingDuringGame.OnAddIncome += SavingDuringGame_OnAddIncome;
    }

    private void OnDestroy() {
        StoreCardBuildingUI.OnSellectButton -= StoreCardBuildingUI_OnSellectButton;
        SavingDuringGame.OnAddIncome -= SavingDuringGame_OnAddIncome;
    }

    private void SavingDuringGame_OnAddIncome(object sender, EventArgs e) {
        CheckIsAvailible();
    }

    private void Bye() {
        CityStatusSO city = WeGiveOutThings.Instance.GetCityStatusSO();
        city.SwichBye(buildingsSO.currentBuilding);
        city.money -= buildingsSO.priceToObtain;
        CheckIsAvailible();

        checkCard();
        CityStatsUI.Instance.UpdateUI();

    }

    private Action checkCard;

    private void StoreCardBuildingUI_OnSellectButton(object sender, StoreCardBuildingUI.OnSellectButtonEventArgs e) {
        buildingsSO = e.buildindsSO;
        SetData();
        CheckIsAvailible();
        checkCard = e.delegat;
        checkCard();
    }

    public void CheckIsAvailible() {
        CityStatusSO citySO = WeGiveOutThings.Instance.GetCityStatusSO();
        if (!citySO.CheckIsBought(buildingsSO.currentBuilding)) {
            if (byeScenario) {
                byeScenario.SetActive(true);
            }
            purchasedScenario.SetActive(false);
            if (citySO.money >= buildingsSO.priceToObtain) {
                byeButton.enabled = true;
                byeButton.gameObject.GetComponent<Image>().sprite = enableSprite;
                priceToBye.color = Color.white;
            } else {
                byeButton.enabled = false;
                byeButton.gameObject.GetComponent<Image>().sprite = disableSprite;
                priceToBye.color = Color.red;
            }
        } else {
            if (byeScenario) {
                byeScenario.SetActive(false);
            }
            purchasedScenario.SetActive(true);
           
        }
    }

    private void SetData() {
        nameText.text = buildingsSO.name;
        moneyBonus.text = buildingsSO.moneyToAddInit.ToString() + "/sec";
        peopleBonus.text = buildingsSO.peopleToAddInit.ToString();
        literacyBonus.text = buildingsSO.literacyToAddInit.ToString();
        moodBonus.text = buildingsSO.moodToAddInit.ToString() + "%";
        priceToBye.text = buildingsSO.priceToObtain.ToString();
    }
}
