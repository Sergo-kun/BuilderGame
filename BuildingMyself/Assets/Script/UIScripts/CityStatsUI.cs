using System;
using TMPro;
using UnityEngine;

public class CityStatsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI peopleText;
    [SerializeField] TextMeshProUGUI literacyText;
    [SerializeField] TextMeshProUGUI moodText;
   
    public static CityStatsUI Instance { get; private set; }


    private void Awake() {
        Instance = this;
        GridSystem.OnPlaceBuilding += GridSystem_OnPlaceBuilding;
    }

    private void OnDestroy() {
        GridSystem.OnPlaceBuilding -= GridSystem_OnPlaceBuilding;
    }
    private void GridSystem_OnPlaceBuilding(object sender, EventArgs e) {
        UpdateUI();
    }

    void Start()
    {
        UpdateUI();
    }


    public void UpdateUI() {
        moneyText.text = GetStat().money.ToString();
        peopleText.text = GetStat().freePeople.ToString();
        literacyText.text = GetStat().literacy.ToString();
        moodText.text = GetStat().mood.ToString();
    }
    private CityStatusSO GetStat() {
      return  WeGiveOutThings.Instance.GetCityStatusSO();
    }
}
            