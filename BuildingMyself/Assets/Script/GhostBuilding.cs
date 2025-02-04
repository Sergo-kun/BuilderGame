using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GhostBuilding : MonoBehaviour {
    public bool isMoving;

  
    [SerializeField] Collider ghostCollider;
    [SerializeField] Color ghostColor;

    private BuildingPlaceUI buildingPlace;

    private void Awake() {
        buildingPlace = GetComponent<BuildingPlaceUI>();
       
    }

   
    private void Start() {
       // CheckIfInsideBuilding();
        CheckForPlacement();
    }
    private void OnEnable() {
        CheckForPlacement();
    }

    public void SetMoving(bool moving) {
      
        isMoving = moving;

       
      
        CheckForPlacement();


    }

    private void CheckForPlacement() {
      //  Debug.Log("moving " + moving + "isInsideProperty " + isInsideProperty + "isGoodDistance" + isGoodDistance);
        if (!isMoving && (isInsideProperty && isOutsideBuilding)) {
            SetGhostColor(ghostColor);
            buildingPlace.ShowHideButtons(true);
        } else {
           // SetGhostColor(Color.red);
            buildingPlace.ShowHideButtons(false);

        }
    }

    private bool isInsideProperty = false;
    private bool isOutsideBuilding = true;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Property") && !isInsideProperty) {
            isInsideProperty = true;
            SetGhostColor(ghostColor);
        }

        if (other.CompareTag("Building") && isInsideProperty) {
            isOutsideBuilding = false;
            SetGhostColor(Color.red);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Property") && isInsideProperty) {
            isInsideProperty = false;
            SetGhostColor(Color.red);
        }

        if (other.CompareTag("Building") && isInsideProperty && !isOutsideBuilding) {
            isOutsideBuilding = true;
            SetGhostColor(ghostColor);
        }
    }
   
    public void SetGhostColor(Color color) {

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers) {

            Material mat = renderer.material;
            mat.color = color;
        }
    }

}
