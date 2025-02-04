using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject[] toggleObjcts;

    [SerializeField] private GameObject buildingMenuContainer;
    [SerializeField] private Button exitButton;
    CityStatusSO.BuildingInfo building;
    int id;
    BuildingsSO constInfo;
   [Header("ImageBuilding")]
    /* [SerializeField] private float scale = 25f;
     [SerializeField] private Vector3 positionBuilding = new Vector3(315, -774, -239);
      private GameObject buildinpParent;*/
    [Header("3D")]
    [SerializeField] private GameObject residenceBuilding3d;
    [SerializeField] private GameObject factoryBuilding3d;
    [SerializeField] private GameObject residential2Building3d;
    [SerializeField] private GameObject residential3Building3d;
    [SerializeField] private GameObject barBuilding3d;
    [SerializeField] private GameObject schoolBuilding3d;
    [SerializeField] private GameObject supermarketBuilding3d;
    [SerializeField] private GameObject cinemaBuilding3d;
    [SerializeField] private GameObject universityBuilding3d;
    [SerializeField] private GameObject nuclearFactoryBuilding3d;
    [SerializeField] private GameObject stadiumBuilding3d;
     
    [Header("SomeStuff")]
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI buildingLvl;
    private List<GameObject> buildings3DList = new List<GameObject>();

    [Header("BeforeStats")]
    [SerializeField] private TextMeshProUGUI currentLvl;
    [SerializeField] private TextMeshProUGUI currentMoney;
    [SerializeField] private TextMeshProUGUI currentPeople;
    [SerializeField] private TextMeshProUGUI currentLiteracy;
    [SerializeField] private TextMeshProUGUI currentMood;
    
    [Header("AfterStats")]
    [SerializeField] private TextMeshProUGUI nextLvl;
    [SerializeField] private TextMeshProUGUI nextMoney;
    [SerializeField] private TextMeshProUGUI nextPeople;
    [SerializeField] private TextMeshProUGUI nextLiteracy;
    [SerializeField] private TextMeshProUGUI nextMood;

    [Header("UpgradeZone")]
    [SerializeField] private TextMeshProUGUI requiredMoney; 
    [SerializeField] private TextMeshProUGUI requiredPeople; 
    [SerializeField] private TextMeshProUGUI requiredLiteracy;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Sprite activeButtonSprite;
    [SerializeField] private Sprite unActiveButtonSprite;


    public static BuildingMenuUI Instance { get; private set; }

    public static event EventHandler OnUpgradeBuilding;

    private bool IsRegymSaved = false;

    private void Awake() {
        Instance = this;
        if (exitButton) {
            exitButton.onClick.AddListener(() => {
                IsRegymSaved = false;
                is3DLoadet = false;
                buildingMenuContainer.SetActive(false);
                GameManager.Instance.CancelOpenMenuRegime();
                SoundComander.Instance.PlayBip();
                ShowObjects();
            });
        }

        if (upgradeButton) {
            upgradeButton.onClick.AddListener(() => {
                SoundComander.Instance.PlayBuy();

                UpgradeBuilding();
            });
        }
        buildings3DList.Add(factoryBuilding3d);
        buildings3DList.Add(residenceBuilding3d);
        buildings3DList.Add(residential2Building3d);
        buildings3DList.Add(residential3Building3d);
        buildings3DList.Add(barBuilding3d);
        buildings3DList.Add(schoolBuilding3d);
        buildings3DList.Add(supermarketBuilding3d);
        buildings3DList.Add(cinemaBuilding3d);
        buildings3DList.Add(universityBuilding3d);
        buildings3DList.Add(nuclearFactoryBuilding3d);
        buildings3DList.Add(stadiumBuilding3d);
        SavingDuringGame.OnAddIncome += SavingDuringGame_OnAddIncome;
    }

    private void OnDestroy() {
        SavingDuringGame.OnAddIncome -= SavingDuringGame_OnAddIncome;
    }
    private void SavingDuringGame_OnAddIncome(object sender, System.EventArgs e) {
        CheckIfAwailible();
    }

    private void Start() {
        buildingMenuContainer.SetActive(false);
    }

    private void UpgradeBuilding() {
        CityStatusSO cityStatus = WeGiveOutThings.Instance.GetCityStatusSO();

        int level = building.level + 1;
        int updateCost = Mathf.CeilToInt(building.moneyCost * constInfo.moneyCostIncrice);
        int literacyCost = Mathf.CeilToInt(building.literacyCost * constInfo.literacyCostIncrice);
        int peopleCost = Mathf.CeilToInt(building.peopleCost * constInfo.peopleCostIncrice);
        int literacyBonus = Mathf.CeilToInt(building.literacyBonus * constInfo.literacyBonusToMultiply);
        int moneyBonus = Mathf.CeilToInt(building.moneyBonus * constInfo.moneyBonusToMultiply);
        int peopleBonus = Mathf.CeilToInt(building.peopleBonus * constInfo.peopleBonusToMultiply);
        int moodBonus = Mathf.CeilToInt(building.moodBonus * constInfo.moodBonusToMultiply);
       
        cityStatus.money -= building.moneyCost;
        cityStatus.freePeople -= building.peopleCost;

        cityStatus.literacy += (literacyBonus - building.literacyBonus);
        cityStatus.people += (peopleBonus - building.peopleBonus);
        cityStatus.freePeople += (peopleBonus - building.peopleBonus);
        cityStatus.mood += Mathf.CeilToInt(moodBonus - building.moodBonus);


        building.SetUpdate(
            level, updateCost, literacyCost, 
            peopleCost, literacyBonus, moneyBonus,
            peopleBonus, moodBonus
        );

        OpenBuildingMenu(id);
        CityStatsUI.Instance.UpdateUI();
        OnUpgradeBuilding?.Invoke(this, EventArgs.Empty);
    }
    private bool is3DLoadet = false;
    public void OpenBuildingMenu(int id) {
        this.id = id;
      //  WeGiveOutThings.Instance.ShowAllTheBuildings();f
        buildingMenuContainer.SetActive(true);
        if (!IsRegymSaved) {
        GameManager.Instance.SetOpenMenuRegime();
            IsRegymSaved = true;
        }
        building = WeGiveOutThings.Instance.GetBuildingMyId(id);
     
        constInfo = WeGiveOutThings.Instance.GetBuildingByEnum(building.currentBuilding);
        if (!is3DLoadet) {
            Show3DImg(building.currentBuilding);
            HideObjects();
            is3DLoadet = true;
        }
        buildingName.text = constInfo.name;
        buildingLvl.text = building.level + "LVL";
        // BeforeStats
        currentLvl.text = building.level + "LVL";
        currentMoney.text = building.moneyBonus + "/sec";
        currentPeople.text = building.peopleBonus.ToString();
        currentLiteracy.text = building.literacyBonus.ToString();
        currentMood.text = building.moodBonus.ToString();
        // AfterStats
        nextLvl.text = building.level + 1 + "LVL";
        nextMoney.text = Mathf.CeilToInt(building.moneyBonus * constInfo.moneyBonusToMultiply) + "/sec";
        nextPeople.text = Mathf.CeilToInt(building.peopleBonus * constInfo.peopleBonusToMultiply) + "";
        nextLiteracy.text = Mathf.CeilToInt(building.literacyBonus * constInfo.literacyBonusToMultiply) + "";
        nextMood.text = Mathf.CeilToInt(building.moodBonus * constInfo.moodBonusToMultiply) + "%";
        // Upgrade zone 
        CheckIfAwailible();
    }

    public void CheckIfAwailible() {
        if (buildingMenuContainer.activeSelf) {
            bool goodMoney = false;
            bool goodPeople = false;
            bool goodLiteracy = false;
            requiredMoney.text = building.moneyCost.ToString();
            requiredPeople.text = building.peopleCost.ToString();
            requiredLiteracy.text = building.literacyCost.ToString();
            if (building.moneyCost > WeGiveOutThings.Instance.GetCityStatusSO().money) {
                requiredMoney.color = Color.red;
                goodMoney = false;
            } else {
                goodMoney = true;
                requiredMoney.color = Color.black;
            }

            if (building.literacyCost > WeGiveOutThings.Instance.GetCityStatusSO().literacy) {
                requiredLiteracy.color = Color.red;
                goodLiteracy = false;
            } else {
                goodLiteracy = true;
                requiredLiteracy.color = Color.black;
            }

            if (building.peopleCost > WeGiveOutThings.Instance.GetCityStatusSO().freePeople) {
                requiredPeople.color = Color.red;
                goodPeople = false;
            } else {
                goodPeople = true;
                requiredPeople.color = Color.black;
            }

            if (goodLiteracy && goodMoney && goodPeople) {
                SetButtnActive();
            } else {
                SetButtonUnactive();
            }
        }
    }

    
    private void SetButtonUnactive() {
      
        upgradeButton.gameObject.GetComponent<Image>().sprite = unActiveButtonSprite;
        upgradeButton.enabled = false;
    }

    private void SetButtnActive() {
      
        upgradeButton.gameObject.GetComponent<Image>().sprite = activeButtonSprite;
        upgradeButton.enabled = true;
    }

    private void HideObjects() {
        objectsToShow.Clear();
        foreach (GameObject go in toggleObjcts) {
            if (go != null && go.activeSelf) {
                objectsToShow.Add(go);
                go.SetActive(false);
            }
        }
    }

    private List<GameObject> objectsToShow = new List<GameObject>();

    private void ShowObjects() {
        foreach (GameObject go in objectsToShow) {
            if (go) {
                go.SetActive(true);
            }
        }
    }

    private void Show3DImg(BuildingsSO.Building enumBuilding) {
        foreach (GameObject go in buildings3DList) {
            if (go.activeSelf) {
                go.SetActive(false);
            }
        }
        switch (enumBuilding) {
            case BuildingsSO.Building.Residential:
                residenceBuilding3d.SetActive(true);
                break;
            case BuildingsSO.Building.Factory:
                factoryBuilding3d.SetActive(true);
                break;
            case BuildingsSO.Building.Residential2:
                residential2Building3d.SetActive(true);
                break;
            case BuildingsSO.Building.Residential3:
                residential3Building3d.SetActive(true);
                break;
            case BuildingsSO.Building.Bar:
                barBuilding3d.SetActive(true);
                break;
            case BuildingsSO.Building.School:
                schoolBuilding3d.SetActive(true);
                break;
            case BuildingsSO.Building.Supermarket:
                supermarketBuilding3d.SetActive(true);
                break;
            case BuildingsSO.Building.Cinema:
                cinemaBuilding3d.SetActive(true);
                break;
            case BuildingsSO.Building.University:
                universityBuilding3d.SetActive(true);
                break;
            case BuildingsSO.Building.NuclearFactory:
                nuclearFactoryBuilding3d.SetActive(true);
                break;
            case BuildingsSO.Building.Stadium:
                stadiumBuilding3d.SetActive(true);
                break;
            default:
                return;
        }
       
        
    }

   
}
