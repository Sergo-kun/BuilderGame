using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SavingDuringGame : MonoBehaviour
{
    [SerializeField] CityStatusSO cityStatusSO;

    public static event EventHandler OnAddIncome;

    public static SavingDuringGame Instance {  get; private set; } 
    private void Awake() {
        Instance = this;
        GetSavedData();


    }
    public void deleteProgress() {
        cityStatusSO.isResidential = false;
        cityStatusSO.isResidential2 = false;
        cityStatusSO.isResidential3 = false;
        cityStatusSO.isFactory = false;
        cityStatusSO.isBar = false;
        cityStatusSO.isSchool = false;
        cityStatusSO.isSupermarket = false;
        cityStatusSO.isCinema = false;
        cityStatusSO.isNuclearFactory = false;
        cityStatusSO.isUniversity = false;
        cityStatusSO.isStadium = false;

        cityStatusSO.isLeftLand = false;
        cityStatusSO.isRightLand = false;
        cityStatusSO.isTopLand = false;
        cityStatusSO.isDownLand = false;

        cityStatusSO.propertyCount = 1;
        cityStatusSO.money = 5000;
        cityStatusSO.people = 10;
        cityStatusSO.freePeople = 10;
        cityStatusSO.mood = 70;
        cityStatusSO.literacy = 10;

        cityStatusSO.buildingsDictionary.Clear();

    }


    public void SetIncome(int income) {
        cityStatusSO.money += income;
        CityStatsUI.Instance.UpdateUI();
        OnAddIncome?.Invoke(this, EventArgs.Empty);

    }

    private void GetSavedData() {
        foreach (KeyValuePair<int, CityStatusSO.BuildingInfo> keyWaluePair in cityStatusSO.buildingsDictionary) {
            Debug.Log(keyWaluePair.Key + "/" + keyWaluePair.Value);
        }
    }

    public void AddSaveBuilding(BuildingsSO buildingsSO, Vector3 position,out int idReturn) {
        int id = cityStatusSO.buildingsDictionary.Count;
        idReturn = id;
        CityStatusSO.BuildingInfo building = new CityStatusSO.BuildingInfo(
            position,
            buildingsSO.currentBuilding,
            1,
           buildingsSO.moneyCost,
       buildingsSO.literacyCost,
       buildingsSO.peopleCost,
           buildingsSO.literacyToAddInit,
           buildingsSO.moneyToAddInit,
           buildingsSO.peopleToAddInit,
           buildingsSO.moodToAddInit);

        cityStatusSO.buildingsDictionary.Add(id, building);
        foreach (KeyValuePair<int, CityStatusSO.BuildingInfo> keyWaluePair in cityStatusSO.buildingsDictionary) {
            Debug.Log(keyWaluePair.Key + "/" + keyWaluePair.Value);
        }
    }



}
