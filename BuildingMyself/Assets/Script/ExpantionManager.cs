using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpantionManager : MonoBehaviour
{

    private const int LAND_PEOPLE_1 = 400;
    private const int LAND_PEOPLE_2 = 800;
    private const int LAND_PEOPLE_3 = 1600;
    private const int LAND_PEOPLE_4 = 3200; 
    
    private const int LAND_PRICE_1 = 10000;
    private const int LAND_PRICE_2 = 20000;
    private const int LAND_PRICE_3 = 30000;
    private const int LAND_PRICE_4 = 40000;

    [SerializeField] List<TextMeshProUGUI> requiredPeopleListText;
    [SerializeField] List<TextMeshProUGUI> prices;

    [SerializeField] List<GameObject> requiredPeopleObjects;
    [SerializeField] List<GameObject> byeLandObjects;

    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] Button topLeftButton;
    [SerializeField] Button downLeftButton;


    [SerializeField] GameObject leftLand;
    [SerializeField] GameObject rightLand;
    [SerializeField] GameObject topLand;
    [SerializeField] GameObject downLand; 
    
    [SerializeField] GameObject leftSeller;
    [SerializeField] GameObject rightSeller;
    [SerializeField] GameObject topSeller;
    [SerializeField] GameObject downSeller;

    private int currentPeoplePrice;
    private int currentPrice;

    private bool enogthPeople = false;

    private enum LandSide {
        leftLand, rightSeller, topLand, downLand
    }


    private void Awake() {
        if (leftButton) {
            leftButton.onClick.AddListener(() => {
                ByeLand(LandSide.leftLand);
            });
        } 
        if (rightButton) {
            rightButton.onClick.AddListener(() => {
                ByeLand(LandSide.rightSeller);
            });
        }
        if (topLeftButton) {
            topLeftButton.onClick.AddListener(() => {
                ByeLand(LandSide.topLand);
            });
        }
        if (downLeftButton) {
            downLeftButton.onClick.AddListener(() => {
                ByeLand(LandSide.downLand);
            });
        }

        GridSystem.OnPlaceBuilding += GridSystem_OnPlaceBuilding;
        BuildingMenuUI.OnUpgradeBuilding += BuildingMenuUI_OnUpgradeBuilding;
        SavingDuringGame.OnAddIncome += SavingDuringGame_OnAddIncome;
    }

    private void OnDestroy() {
        GridSystem.OnPlaceBuilding -= GridSystem_OnPlaceBuilding;
        BuildingMenuUI.OnUpgradeBuilding -= BuildingMenuUI_OnUpgradeBuilding;
        SavingDuringGame.OnAddIncome -= SavingDuringGame_OnAddIncome;
    }


    private void ByeLand(LandSide landSide) {
        CityStatusSO city = WeGiveOutThings.Instance.GetCityStatusSO();
        if (currentPrice <= city.money) {
            switch (landSide) {
                case LandSide.leftLand:
                    city.isLeftLand = true;
                    break;
                case LandSide.rightSeller:
                    city.isRightLand = true;
                    break;
                case LandSide.topLand:
                    city.isTopLand = true;
                    break;
                case LandSide.downLand:
                    city.isDownLand = true;
                    break;
                default:
                    break;
            }
            city.propertyCount++;
            city.money -= currentPrice;
            CityStatsUI.Instance.UpdateUI();
            UpdateInfo();
        } else {
           
        }
    }

    private void SavingDuringGame_OnAddIncome(object sender, System.EventArgs e) {
        if (enogthPeople) {
            UpdateInfo();
        }
    }

    private void BuildingMenuUI_OnUpgradeBuilding(object sender, System.EventArgs e) {
        if (!enogthPeople) { 
        UpdateInfo();
        }
    }

    private void GridSystem_OnPlaceBuilding(object sender, System.EventArgs e) {
        if (!enogthPeople) {
            UpdateInfo();
        }
    }

    private void Start() {
        UpdateInfo();
    }

    private void UpdateInfo() {
        CityStatusSO cityStatusSO = WeGiveOutThings.Instance.GetCityStatusSO();
        ChekIsLandBought(cityStatusSO);
        ChekPeople(cityStatusSO);

    }

    private void ChekPeople(CityStatusSO cityStatusSO) {
        switch (cityStatusSO.propertyCount) {
            case 1:
                currentPeoplePrice = LAND_PEOPLE_1;
                currentPrice = LAND_PRICE_1;
                break;
            case 2:
                currentPeoplePrice = LAND_PEOPLE_2;
                currentPrice = LAND_PRICE_2;
                break;
            case 3:
                currentPeoplePrice = LAND_PEOPLE_3;
                currentPrice = LAND_PRICE_3;
                break;
            case 4:
                currentPeoplePrice= LAND_PEOPLE_4;
                currentPrice = LAND_PRICE_4;
                break;
            default:
                break;
        }

    if (currentPeoplePrice <= cityStatusSO.people) {
            enogthPeople = true;
            AllowByuing();
            SetPrice();

        } else {
            enogthPeople = false;
            CancelBuying();
            SetPeople();
        }
    }

    private void SetPrice() {
        foreach (TextMeshProUGUI text in prices) {
            if (text) {

            text.text = currentPrice.ToString();
            }
        }
    }

    private void SetPeople() {
        foreach (TextMeshProUGUI text in requiredPeopleListText) {
            if (text) {
                text.text = currentPeoplePrice.ToString();
            }
            }

    }

    private void ChekIsLandBought(CityStatusSO cityStatusSO) {

      if (leftLand)  leftLand.SetActive(cityStatusSO.isLeftLand);
        if (rightLand) rightLand.SetActive(cityStatusSO.isRightLand);
        if (topLand) topLand.SetActive(cityStatusSO.isTopLand);
        if (downLand) downLand.SetActive(cityStatusSO.isDownLand);

        if (leftSeller) leftSeller.SetActive(!cityStatusSO.isLeftLand);
        if (rightSeller) rightSeller.SetActive(!cityStatusSO.isRightLand);
        if (topSeller) topSeller.SetActive(!cityStatusSO.isTopLand);
        if (downSeller) downSeller.SetActive(!cityStatusSO.isDownLand);

    
    }
    private void AllowByuing() {
        HideRequiredPeopleObjects();
        ShowByeLandObjects();
    }

    private void CancelBuying() {
        ShowRequiredPeopleObjects();
        HideByeLandObjects(); 
    }

    private void ShowByeLandObjects() {
        foreach (GameObject obj in byeLandObjects) {
            if (obj) {
                obj.SetActive(true);
            }
        }
    }

    private void HideByeLandObjects() {
        foreach (GameObject obj in byeLandObjects) {
          if (obj) {

                obj.SetActive(false);
            }
            
        }
    }

    private void ShowRequiredPeopleObjects() {
        foreach (GameObject obj in requiredPeopleObjects) {
            if (obj) {
                obj.SetActive(true);
            }
        }
    }
    private void HideRequiredPeopleObjects() {
        foreach (GameObject obj in requiredPeopleObjects) {
            if (obj) {
                obj.SetActive(false);
            }
        }
    }



}
