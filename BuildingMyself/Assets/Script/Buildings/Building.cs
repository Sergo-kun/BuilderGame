using System;
using UnityEngine;
using UnityEngine.UI;
using static CityStatusSO;


public class Building : MonoBehaviour, ITouchable {

    public Info info;
    [SerializeField] BuildingUI buildingUI;
    [SerializeField] bool isMeria = false;

    public Action delegat;

    private void Awake() {
        info = new Info();

        info.initScale = transform.localScale;
    }

    public void SetSavedValue(int id) {
        info.id = id;
    }
    private void OnEnable() {
        Untouch();
    }

    public void GetTouched() {
        delegat = Untouch;
        transform.localScale = new Vector3(transform.localScale.x + .01f, transform.localScale.y + .01f, transform.localScale.z + .01f);
        if (!isMeria) {
            buildingUI.ShowInfo(info.id, delegat);
        }


    }

    public void Untouch() {
        // Downscale(gameObject, info.initScale);
        transform.localScale = info.initScale;
        if (buildingUI) {
            buildingUI.HideInfo();
        }
    }

    public struct Info {
        public Vector3 initScale;


        public int id;

    }






}
