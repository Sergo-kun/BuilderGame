using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsSO", menuName = "Scriptable Objects/BuildingsSO")]
public class BuildingsSO : ScriptableObject {
    public enum Building { 
        Residential,
        Factory,
        Residential2,
        Residential3,
        Bar,
        School,
        Supermarket, 
        Cinema,
        University,
        NuclearFactory,
        Stadium
    }
    
    public GameObject buildinf3DUI;
    public Building currentBuilding;
    public string name;
    public GameObject ghostObject;
    public GhostBuilding ghostBuildingScript;
    public GameObject perfabBuilding;

    public int priceToObtain;
    public int moneyCost;
    public int peopleCost;
    public int literacyCost;


    public float moneyCostIncrice;
    public float literacyCostIncrice;
    public float peopleCostIncrice;
   

    public int moneyToAddInit;
    public int peopleToAddInit;
    public float moodToAddInit;
    public int literacyToAddInit;
    
    public float moneyBonusToMultiply;
    public float literacyBonusToMultiply;
    public float peopleBonusToMultiply;
    public float moodBonusToMultiply;

    

}
