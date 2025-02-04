using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeriaHubUI : MonoBehaviour
{

    [SerializeField] Button openHubButton;
    [SerializeField] List<GameObject> objToToggleList = new List<GameObject>();

    [SerializeField] GameObject meriaHubObj;
    [SerializeField] Button exitButton;


    public static MeriaHubUI Instance { get; private set; }


    private void Awake() {
        Instance = this;
        meriaHubObj.SetActive(false);
        openHubButton.gameObject.SetActive(true);
       if (openHubButton) {
            openHubButton.onClick.AddListener(() => {
                SoundComander.Instance.PlayBip();
                OpenMeria();
            });
        }

        if (exitButton) {
            exitButton.onClick.AddListener(() => {
                meriaHubObj.SetActive(false);
                GameManager.Instance.CancelOpenMenuRegime();
                SoundComander.Instance.PlayBip();
                ShowObjects();
            });
        }
        BuildingUI.OnOpenMeria += BuildingUI_OnOpenMeria;
    }

    private void BuildingUI_OnOpenMeria(object sender, EventArgs e) {
        OpenMeria();
    }

    public void OpenMeria() {
        meriaHubObj.SetActive(true);
        GameManager.Instance.SetOpenMenuRegime();
        HideObjects();
    }

    private List<GameObject> objectsToShowList;
    public void HideObjects() {
        objectsToShowList = new List<GameObject>();
        foreach (GameObject obj in objToToggleList) { 
            if (obj.activeSelf) {
                objectsToShowList.Add(obj);
                obj.SetActive(false);
            }
        }
    }

    public void ShowObjects() {
        foreach (GameObject obj in objectsToShowList) {
                obj.SetActive(true);          
        }
    }


}
