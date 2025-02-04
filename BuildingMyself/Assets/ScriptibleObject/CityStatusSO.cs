using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableBuildingInfo {
    public int id; // The key of the dictionary
    public Vector3 position;
    public BuildingsSO.Building currentBuilding;
    public int level;
    public int moneyCost;
    public int literacyCost;
    public int peopleCost;
    public int literacyBonus;
    public int moneyBonus;
    public int peopleBonus;
    public float moodBonus;

    public SerializableBuildingInfo(int id, CityStatusSO.BuildingInfo info) {
        this.id = id;
        this.position = info.position;
        this.currentBuilding = info.currentBuilding;
        this.level = info.level;
        this.moneyCost = info.moneyCost;
        this.literacyCost = info.literacyCost;
        this.peopleCost = info.peopleCost;
        this.literacyBonus = info.literacyBonus;
        this.moneyBonus = info.moneyBonus;
        this.peopleBonus = info.peopleBonus;
        this.moodBonus = info.moodBonus;
    }
}
[CreateAssetMenu(fileName = "CityStatusSO", menuName = "Scriptable Objects/CityStatusSO")]
public class CityStatusSO : ScriptableObject {
    public class BuildingInfo {
        public Vector3 position;
        public BuildingsSO.Building currentBuilding;
        public int level;
        public int moneyCost;
        public int literacyCost;
        public int peopleCost;

        public int literacyBonus;
        public int moneyBonus;
        public int peopleBonus;
        public float moodBonus;

        public BuildingInfo(Vector3 position,
            BuildingsSO.Building currentBuilding,
            int level, int udateCost, int literacyCost,
               int peopleCost, int literacyBonus,
                int moneyBonus, int peopleBonus, float moodBonus
            ) {
            this.position = position;
            this.currentBuilding = currentBuilding;
            this.level = level;
            this.moneyCost = udateCost;
            this.literacyCost = literacyCost;
            this.peopleCost = peopleCost;

            this.literacyBonus = literacyBonus;
            this.moneyBonus = moneyBonus;
            this.peopleBonus = peopleBonus;
            this.moodBonus = moodBonus;
        }

        public void SetUpdate(int level, int udateCost, int literacyCost,
               int peopleCost, int literacyBonus,
                int moneyBonus, int peopleBonus, float moodBonus) {
            this.level = level;
            this.moneyCost = udateCost;
            this.literacyCost = literacyCost;
            this.peopleCost = peopleCost;

            this.literacyBonus = literacyBonus;
            this.moneyBonus = moneyBonus;
            this.peopleBonus = peopleBonus;
            this.moodBonus = moodBonus;
            
        }

    }
    
    public int level;
    public int money;
    public int people;
    public int freePeople;
    public int mood;
    public int literacy;
    public int propertyCount;

    public bool isLeftLand = false;
    public bool isRightLand = false;
    public bool isTopLand = false;
    public bool isDownLand = false;
  
    public Dictionary<int, BuildingInfo> buildingsDictionary = new Dictionary<int, BuildingInfo>();
    [SerializeField] public List<SerializableBuildingInfo> serializedBuildings = new List<SerializableBuildingInfo>();
   
    public bool isResidential = false;
    public bool isFactory = false;
    public bool isResidential2 = false;
    public bool isResidential3 = false;
    public bool isBar = false;
    public bool isSchool = false;
    public bool isSupermarket = false;
    public bool isCinema = false;
    public bool isNuclearFactory = false;
    public bool isUniversity = false;
    public bool isStadium = false;

    public void BeforeSaving() {
        serializedBuildings.Clear();
        foreach (var pair in buildingsDictionary) {
            serializedBuildings.Add(new SerializableBuildingInfo(pair.Key, pair.Value));
        }
    }

    public void AfterLoading() {
        buildingsDictionary.Clear();
        foreach (var item in serializedBuildings) {
            buildingsDictionary[item.id] = new BuildingInfo(
                item.position, item.currentBuilding, item.level,
                item.moneyCost, item.literacyCost, item.peopleCost,
                item.literacyBonus, item.moneyBonus, item.peopleBonus, item.moodBonus
            );
        }
    }


    public void SwichBye(BuildingsSO.Building enumInfo) {
        switch (enumInfo) {
            case BuildingsSO.Building.Residential:
                isResidential = true;
                break;
            case BuildingsSO.Building.Factory:
                isFactory = true;
                break;
            case BuildingsSO.Building.Residential2:
                isResidential2 = true;
                break;
            case BuildingsSO.Building.Residential3:
                isResidential3 = true;
                break;
            case BuildingsSO.Building.Bar:
                isBar = true;
                break;
            case BuildingsSO.Building.School:
                isSchool = true;
                break;
            case BuildingsSO.Building.Supermarket:
                isSupermarket = true;
                break;
            case BuildingsSO.Building.Cinema:
                isCinema = true;
                break;
            case BuildingsSO.Building.University:
                isUniversity = true;
                break;
            case BuildingsSO.Building.NuclearFactory:
                isNuclearFactory = true;
                break;
            case BuildingsSO.Building.Stadium:
                isStadium = true;
                break;

            default:
                return;
        }
    }
    public bool CheckIsBought(BuildingsSO.Building enumInfo) {
        switch (enumInfo) {
            case BuildingsSO.Building.Residential:
                return isResidential;

            case BuildingsSO.Building.Factory:
                return isFactory;
            case BuildingsSO.Building.Residential2:
                return isResidential2;

            case BuildingsSO.Building.Residential3:
                return isResidential3;

            case BuildingsSO.Building.Bar:
                return isBar;

            case BuildingsSO.Building.School:
                return isSchool;

            case BuildingsSO.Building.Supermarket:
                return isSupermarket;

            case BuildingsSO.Building.Cinema:
                return isCinema;

            case BuildingsSO.Building.University:
                return isUniversity;

            case BuildingsSO.Building.NuclearFactory:
                return isNuclearFactory;

            case BuildingsSO.Building.Stadium:
                return isStadium;

            default:
                return false;
        }


    }
}
