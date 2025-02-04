using System.Collections.Generic;
using UnityEngine;

public class WeGiveOutThings : MonoBehaviour
{
    [SerializeField] List<BoxCollider> propertyCollidersList = new List<BoxCollider>();

    [SerializeField] CityStatusSO cityStatusSO;

    [SerializeField] BuildingsSO factorySO;
    [SerializeField] BuildingsSO residentialSO;

    public List<BuildingsSO> buildingsSOList = new List<BuildingsSO>();
       

    

    public static WeGiveOutThings Instance { get; private set; }

    private void Awake() {
        Instance = this;
       /* buildingsSOList.Add(factorySO);
        buildingsSOList.Add(residentialSO);*/
    }

    public List<BoxCollider> GetPropertyCollidersList() {
        return propertyCollidersList;
    }

    
    public CityStatusSO GetCityStatusSO() {
        return cityStatusSO;
    }
    public CityStatusSO.BuildingInfo GetBuildingMyId(int id) {
        if (cityStatusSO.buildingsDictionary.TryGetValue(id, out CityStatusSO.BuildingInfo buildingInfo)) { return buildingInfo; }
        return null;
    }

    public void ShowAllTheBuildings() {
        foreach (KeyValuePair<int, CityStatusSO.BuildingInfo> keyValuePair in cityStatusSO.buildingsDictionary) {
            Debug.Log(keyValuePair.Key + "|||" + keyValuePair.Value.currentBuilding);
        }
    }

    public BuildingsSO GetBuildingByEnum(BuildingsSO.Building enumBuilding) {
        foreach (BuildingsSO buildingInfo in buildingsSOList) {
            if (buildingInfo && buildingInfo.currentBuilding == enumBuilding) {
                return buildingInfo;
            }
        }
        return null;
    }
    }
