using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsSelectionHolderUI : MonoBehaviour
{
    [SerializeField] List<BuildingSelectButtonUI> buildindButtonsList = new List<BuildingSelectButtonUI>();
    public static BuildingsSelectionHolderUI Instance { get; private set; }

    [Serializable] public class ObjectAndSO {
        public GameObject card;
        public BuildingsSO buildingsSO;
    }

    [SerializeField] private List<ObjectAndSO> objectAndSOList = new List<ObjectAndSO>();

    private Dictionary<GameObject, BuildingsSO> objectAndSODictionary;


    private void Awake() {
        Instance = this;
        objectAndSODictionary = new Dictionary<GameObject, BuildingsSO>();
        foreach (var obj in objectAndSOList) {
            objectAndSODictionary.Add(obj.card, obj.buildingsSO);
        }
    }

    private void OnEnable() {
        CheckIsBought();
    }

    private void CheckIsBought() {
        CityStatusSO city = WeGiveOutThings.Instance.GetCityStatusSO();
        foreach (KeyValuePair<GameObject, BuildingsSO> keyValue in objectAndSODictionary) {
            if (city.CheckIsBought(keyValue.Value.currentBuilding)) {
                keyValue.Key.SetActive(true);
            } else {
                keyValue.Key.SetActive(false);
            }
        }
    }

    public void UntogleBordersUI() {
        foreach (BuildingSelectButtonUI buildindButton in buildindButtonsList) {
            if (buildindButton != null) {
                buildindButton.HideBorder();
            }
        }
    }
}
