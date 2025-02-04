using Unity.VisualScripting;
using UnityEngine;

public class Factory : MonoBehaviour {

    private int id;

    private float timer;
    private int earning = 0;
  
    private void Awake() {
       
        GridSystem.OnPlaceBuilding += GridSystem_OnPlaceBuilding;
        BuildingMenuUI.OnUpgradeBuilding += BuildingMenuUI_OnUpgradeBuilding;
    }

    private void OnDestroy() {
        GridSystem.OnPlaceBuilding -= GridSystem_OnPlaceBuilding;
        BuildingMenuUI.OnUpgradeBuilding -= BuildingMenuUI_OnUpgradeBuilding;
    }

    private void BuildingMenuUI_OnUpgradeBuilding(object sender, System.EventArgs e) {
        CalculateMoodEarnings();
    }

    private void GridSystem_OnPlaceBuilding(object sender, System.EventArgs e) {
        CalculateMoodEarnings();
    }

    private void Start() {
        id = GetComponent<Building>().info.id;
        CalculateMoodEarnings();
    }


    // upgate if upgrade building
    // upgrade if builds building (check)
    private void CalculateMoodEarnings() {
      
        earning = WeGiveOutThings.Instance.GetBuildingMyId(id).moneyBonus; // Base earnings
        int mood = WeGiveOutThings.Instance.GetCityStatusSO().mood; // Get mood value

        int moodEffect = 0;

        if (mood >= 50) {
            // Mood above 50% → Unlimited positive earnings boost
            moodEffect = mood - 50; // Example: 60% mood → +10%, 200% mood → +150%
        } else if (mood <= 40) {
            // Mood below 40% → Negative earnings impact (capped at -40%)
            moodEffect = Mathf.Max(mood - 50, -40);
        }

        // Apply mood effect to earnings
        earning += Mathf.CeilToInt((earning * moodEffect) / 100);
       
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= 1f) {
          
            SavingDuringGame.Instance.SetIncome(earning);
            timer = 0;
        }
    }
}
