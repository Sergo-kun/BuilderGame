using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private CityStatusSO cityStatusSO;

    private void Awake() {
       
    }

    private void Start() {
        WeGiveOutThings.Instance.ShowAllTheBuildings();
        LoadData();
    }

    private void LoadData() {
        loadingScreen.SetActive(true);
        // cityStatusSO = WeGiveOutThings.Instance.GetCityStatusSO();
        if (cityStatusSO.buildingsDictionary.Count > 0) {
            foreach (/*KeyValuePair<int, CityStatusSO.BuildingInfo>*/SerializableBuildingInfo keyValuePair in cityStatusSO.serializedBuildings) {
              
             
                BuildingsSO buildingConst = WeGiveOutThings.Instance.GetBuildingByEnum(keyValuePair.currentBuilding);
                GameObject building = Instantiate(buildingConst.perfabBuilding, keyValuePair.position, Quaternion.identity);
                building.GetComponentInChildren<Building>().SetSavedValue(keyValuePair.id);
            }
        }
        loadingScreen.SetActive(false);

    }
}
