using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event EventHandler OnBuildingRegime;
    public event EventHandler OnMovingRegime;

    public enum Regimes {
        BuildRegime,
        MoveRegime,
        OpenUIRegime
    }

    public Regimes currentRegimes;

    public void Start() {
        SwichToMoveRegime();
    }



    private void Awake() {
        Instance = this;
        currentRegimes = Regimes.MoveRegime;
    }

    private List<Regimes> previousEnumList = new List<Regimes>();
    public void SetOpenMenuRegime() {
        previousEnumList.Clear();
        previousEnumList.Add(currentRegimes);
        currentRegimes = Regimes.OpenUIRegime;
    }

    public void CancelOpenMenuRegime() {
        currentRegimes = previousEnumList[0];
    }

    public void SwichToBuildRegime() {
        currentRegimes = Regimes.BuildRegime;
        OnBuildingRegime?.Invoke(this, EventArgs.Empty);
        ShowPropertColliders();
    } 
    
    public void SwichToMoveRegime() {     
        currentRegimes = Regimes.MoveRegime;
        OnMovingRegime?.Invoke(this, EventArgs.Empty);
        HidePropertColliders();
    }

    private void HidePropertColliders() {
        List<BoxCollider> list = WeGiveOutThings.Instance.GetPropertyCollidersList();
        foreach (BoxCollider collider in list) {
            collider.enabled = false;
        }
    }
    private void ShowPropertColliders() {
        List<BoxCollider> list = WeGiveOutThings.Instance.GetPropertyCollidersList();
        foreach (BoxCollider collider in list) {
            collider.enabled = true;
        }
    }
}
