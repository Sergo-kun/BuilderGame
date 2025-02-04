using UnityEngine;
using UnityEngine.EventSystems;

public class GameUtils : MonoBehaviour
{

    public static GameUtils Instance { get; private set; }
    private void Awake() {
        Instance = this;
    }
    public bool IsTouchOverUI(Touch touch) {
       
            return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
       
    }
}
