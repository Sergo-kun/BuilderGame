using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollAndPinch : MonoBehaviour
{

    [SerializeField] private Vector2 horizontalRestriction = new Vector2(-80, 80);
    [SerializeField] private Vector2 verticalRestriction = new Vector2(-70, 70);

    public Camera camera;
    public bool Rotate;
    protected Plane Plane;

    private bool isMowingCamera;

    private void Awake()
    {
        isMowingCamera = false;
        if (camera == null)
            camera = Camera.main;
    }

    private void Update() {
        if (GameManager.Instance.currentRegimes == GameManager.Regimes.MoveRegime) {
          //  if (!IsCameraOutsideTheBounds()) {
                MoveHandler();
            ClampCameraPosition();
          //  } else {
            /*   Vector3 cameraPos = camera.transform.position;

               // Clamp the camera position to stay within the horizontal and vertical bounds
               cameraPos.x = Mathf.Clamp(cameraPos.x, horizontalRestriction.x, horizontalRestriction.y);
               cameraPos.z = Mathf.Clamp(cameraPos.z, verticalRestriction.x, verticalRestriction.y);*/

            // Apply the clamped position
            // camera.transform.position = cameraPos;
            /* if (IsCameraOutsideTheLeft()) {
                 camera.transform.position = new Vector3(horizontalRestriction.x + 1, camera.transform.position.y, camera.transform.position.z);
             }
             if (IsCameraOutsideTheRight()) {
                 camera.transform.position = new Vector3(horizontalRestriction.y - 1, camera.transform.position.y, camera.transform.position.z);
             }
             if (IsCameraOutsideTheTop()) {
                 camera.transform.position = new Vector3(camera.transform.position.x , camera.transform.position.y, verticalRestriction.y - 1);

             }
             if (IsCameraOutsideTheDown()) {
                 camera.transform.position = new Vector3(camera.transform.position.x , camera.transform.position.y, verticalRestriction.x + 1);

             }*/
            // }
        }
        if (GameManager.Instance.currentRegimes == GameManager.Regimes.MoveRegime) {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended && !isMowingCamera && !IsTouchOverUI(touch)) {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hit)) {
                        Debug.Log(hit.collider.name);
                        if (hit.collider.TryGetComponent(out ITouchable building)) {
                            {
                                if (touchetBuilding != null) {

                                    touchetBuilding.Untouch();
                                }
                                Debug.Log("TryGetComponent");
                                building.GetTouched();
                                touchetBuilding = building;
                            }
                        } else {
                            if (touchetBuilding != null) {

                                touchetBuilding.Untouch();
                            }
                        }
                    } else {
                        if (touchetBuilding != null) {

                            touchetBuilding.Untouch();
                        }
                    }
                }
            }
        }
        }

    private void ClampCameraPosition() {
        Vector3 cameraPos = camera.transform.position;

        cameraPos.x = Mathf.Clamp(cameraPos.x, horizontalRestriction.x, horizontalRestriction.y);
        cameraPos.z = Mathf.Clamp(cameraPos.z, verticalRestriction.x, verticalRestriction.y);

        camera.transform.position = cameraPos;
    }
    ITouchable touchetBuilding;
   
    private bool IsCameraOutsideTheLeft() {
        if (camera.transform.position.x < horizontalRestriction.x) {
            Debug.Log("IsCameraOutsideTheLeft");
            return true;
        } else {
            return false;
        }
    }
    private bool IsCameraOutsideTheRight() {
        if (camera.transform.position.x > horizontalRestriction.y) {
            Debug.Log("IsCameraOutsideTheRight");
            return true;
        } else {
            return false;
        }
    }
    private bool IsCameraOutsideTheTop() {
        if (camera.transform.position.z > verticalRestriction.y) {
            Debug.Log("IsCameraOutsideTheTop");
            return true;
        } else {
            return false;
        }
    } 
    private bool IsCameraOutsideTheDown() {
        if (camera.transform.position.z < verticalRestriction.x) {
            Debug.Log("IsCameraOutsideTheDown");
            return true;
        } else {
            return false;
        }

    }
    private bool IsCameraOutsideTheBounds() {
        if (IsCameraOutsideTheDown() || IsCameraOutsideTheTop() || IsCameraOutsideTheLeft() || IsCameraOutsideTheRight()) {
            Debug.Log("Is outside the bounds");
            return true;
        } else {
            return false;
        }
    }

    private void MoveHandler() {
        Plane.SetNormalAndPosition(transform.up, transform.position);

        var Delta1 = Vector3.zero;

        // Scroll Handling
        if (Input.touchCount >= 1) {
            Touch touch = Input.GetTouch(0);
            if (!IsTouchOverUI(touch)) {
                Delta1 = PlanePositionDelta(touch);

                if (touch.phase == TouchPhase.Moved) {
                    Vector3 newPosition = camera.transform.position + Delta1;

                    // Apply movement *before* setting position to prevent overshooting
                    newPosition.x = Mathf.Clamp(newPosition.x, horizontalRestriction.x, horizontalRestriction.y);
                    newPosition.z = Mathf.Clamp(newPosition.z, verticalRestriction.x, verticalRestriction.y);

                    camera.transform.position = newPosition;

                    if (!isMowingCamera) {
                        StartCoroutine(StartTouch());
                    }
                }

                if (touch.phase == TouchPhase.Ended && isMowingCamera) {
                    StartCoroutine(EndTouch());
                }
            }
        }
        /*
        //Pinch
        if (Input.touchCount >= 2)
        {
            var pos1  = PlanePosition(Input.GetTouch(0).position);
            var pos2  = PlanePosition(Input.GetTouch(1).position);
            var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            //calc zoom
            var zoom = Vector3.Distance(pos1, pos2) /
                       Vector3.Distance(pos1b, pos2b);

            //edge case
            if (zoom == 0 || zoom > 10)
                return;

            //Move cam amount the mid ray
            Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);

            if (Rotate && pos2b != pos2)
                Camera.transform.RotateAround(pos1, Plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal));
        }
        */
    }

    private IEnumerator EndTouch() {
        yield return new WaitForSeconds(.1f);
        isMowingCamera = false;
    }
    private IEnumerator StartTouch() {
        yield return new WaitForSeconds(.05f);
        isMowingCamera = true;
    }

    public bool IsTouchOverUI(Touch touch) {

        return EventSystem.current.IsPointerOverGameObject();

    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved && !IsTouchOverUI(touch))
            return Vector3.zero;

        //delta
        var rayBefore = camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = camera.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //not on plane
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = camera.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }

}
