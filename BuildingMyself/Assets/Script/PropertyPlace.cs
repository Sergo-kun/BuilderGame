using UnityEngine;

public class PropertyPlace : MonoBehaviour
{
    public Collider objectCollider;
  

    public static PropertyPlace Instance {  get; private set; }

    private void Awake() {
        objectCollider = GetComponent<Collider>();
        Instance = this;
    }
    void Start()
    {
      //  bounds = objectCollider.bounds;
    }
  
}
