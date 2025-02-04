using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridSystem : MonoBehaviour {
  //  public GameObject ghostPerfab;
    public float gridSize;
  //  public GameObject perfabToPlace;
    private GameObject ghostObject;
        

    private BuildingsSO buildingSO;

    public void AddPerfabs(BuildingsSO buildingSO) {
        //  this.ghostPerfab = ghostPerfab;
        //   perfabToPlace = perfab;
        this.buildingSO = buildingSO;

        if (ghostObject) {

            CreateGhostObject();
        }

    }
    public static GridSystem Instance { get; private set; }
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        //  CreateGhostObject();
        GameManager.Instance.OnBuildingRegime += Instance_OnBuildingRegime;
        GameManager.Instance.OnMovingRegime += Instance_OnMovingRegime;
    }

    private void Instance_OnMovingRegime(object sender, System.EventArgs e) {
        Destroy(ghostObject);
    }

    private void Instance_OnBuildingRegime(object sender, System.EventArgs e) {
        CreateGhostObject();
    }


    private void Update() {

        if (GameManager.Instance.currentRegimes == GameManager.Regimes.BuildRegime) {
            if (ghostObject) {
                UpdateGhostPosition();

            }
        }
    }

    void CreateGhostObject() {
        if (ghostObject) {
            Destroy(ghostObject);
        }

        ghostObject = Instantiate(buildingSO.ghostObject);
        ghostScript = ghostObject.GetComponent<GhostBuilding>();
     //   ghostObject.GetComponent<Collider>().enabled = false;
        ghostObject.transform.position = new Vector3(ghostObject.transform.position.x, -5f, ghostObject.transform.position.z);
        Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();
    }



    public bool IsTouchOverUI(Touch touch) {

        return EventSystem.current.IsPointerOverGameObject();

    }
    GhostBuilding ghostScript;

    void UpdateGhostPosition() {

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began) {
                if (!IsTouchOverUI(touch)) {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);

                    Plane groundPlane = new Plane(Vector3.up, Vector3.up * -5); // Plane at Y = -5
                    if (groundPlane.Raycast(ray, out float enter)) {
                        Vector3 hitPoint = ray.GetPoint(enter);

                      //  float gridX = Mathf.Round(hitPoint.x / gridSize) * gridSize;
                        // float gridY = Mathf.Round(point.y / gridSize) * gridSize;
                      //  float gridZ = Mathf.Round(hitPoint.z / gridSize) * gridSize;

                        Vector3 snappedPosition = new Vector3(hitPoint.x, -5, hitPoint.z);

                        ghostObject.transform.position = snappedPosition;



                        if (ghostScript && !ghostScript.isMoving) {
                            ghostScript.SetMoving(true);

                        }

                   
                    }
                }
            }
        } else {
            if (ghostScript && ghostScript.isMoving) {
                ghostScript.SetMoving(false);
            }
        }

    }



    public void SetGhostColor(Color color) {

        Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers) {

            Material mat = renderer.material;
            mat.color = color;
        }
    }

    public void PlaceObject() {

        Vector3 placementPosition = ghostObject.transform.position;
        GameObject building = Instantiate(buildingSO.perfabBuilding, placementPosition, Quaternion.identity);
        
        SavingDuringGame.Instance.AddSaveBuilding(buildingSO, placementPosition,out int id);
        Building buildingScript = building.GetComponentInChildren<Building>();
        buildingScript.SetSavedValue(id);
        ChangeRecurces();
        Destroy(ghostObject);
    }

    public static event EventHandler OnPlaceBuilding;

    private void ChangeRecurces() {
        CityStatusSO cityStatus = WeGiveOutThings.Instance.GetCityStatusSO();
        cityStatus.money -= buildingSO.moneyCost;
        cityStatus.freePeople -= buildingSO.peopleCost;

        cityStatus.mood += (int)buildingSO.moodToAddInit;
        cityStatus.people += buildingSO.peopleToAddInit;
        cityStatus.freePeople += buildingSO.peopleToAddInit;
        cityStatus.literacy += buildingSO.literacyToAddInit;
        OnPlaceBuilding?.Invoke(this, EventArgs.Empty);
      //  CityStatsUI.Instance.UpdateUI();

    }
}


