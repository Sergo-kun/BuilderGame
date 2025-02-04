using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectButtonUI : MonoBehaviour {
    private Button button;
    [SerializeField] BuildingsSO buildingsSO;
    [SerializeField] GameObject selectedMark;

    [SerializeField] TextMeshProUGUI moneyPrice;
    [SerializeField] TextMeshProUGUI freePeoplePrice;


    private void Awake() {
        selectedMark.gameObject.SetActive(false);
        button = GetComponent<Button>();


        if (button) {
            button.onClick.AddListener(() => {
                BuildingsSelectionHolderUI.Instance.UntogleBordersUI();
                selectedMark.gameObject.SetActive(true);
                SoundComander.Instance.PlayBip();
                GridSystem.Instance.AddPerfabs(buildingsSO);
                GameManager.Instance.SwichToBuildRegime();
            });
        }
        GridSystem.OnPlaceBuilding += GridSystem_OnPlaceBuilding;
        SavingDuringGame.OnAddIncome += SavingDuringGame_OnAddIncome;
    }

    private void OnDestroy() {
        GridSystem.OnPlaceBuilding -= GridSystem_OnPlaceBuilding;
        SavingDuringGame.OnAddIncome -= SavingDuringGame_OnAddIncome;
    }

    private void SavingDuringGame_OnAddIncome(object sender, System.EventArgs e) {
        CheckIfAvailible();
    }

    private void GridSystem_OnPlaceBuilding(object sender, System.EventArgs e) {
        CheckIfAvailible();
    }

    public void HideBorder() {
        selectedMark.gameObject.SetActive(false);
    }

    private void OnEnable() {
     
        CheckIfAvailible();
        SetPrice();
    }

 

    public void SetPrice() {
        moneyPrice.text = buildingsSO.moneyCost.ToString();
        freePeoplePrice.text = buildingsSO.peopleCost.ToString();
    }

    public void CheckIfAvailible() {
        if (WeGiveOutThings.Instance) {
            CityStatusSO cityStatus = WeGiveOutThings.Instance.GetCityStatusSO();
            if (buildingsSO.moneyCost > cityStatus.money ||
                buildingsSO.peopleCost > cityStatus.freePeople
             ) {
                SetBuildingCostColor(Color.red);
                button.enabled = false;
            } else {
                SetBuildingCostColor(Color.white);
                button.enabled = true;
            }
        }
    }

    public void SetBuildingCostColor(Color color) { // to make red if no money

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers) {

            Material mat = renderer.material;
            mat.color = color;
        }
    }


}
