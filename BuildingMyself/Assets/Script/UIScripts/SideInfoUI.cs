using TMPro;
using UnityEngine;

public class SideInfoUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI populationText;
    [SerializeField] private TextMeshProUGUI propertyText;

    private void OnEnable() {
        SetValue();
    }
    private void SetValue() {
        if (WeGiveOutThings.Instance) {
            CityStatusSO cityStatusSO = WeGiveOutThings.Instance.GetCityStatusSO();
            populationText.text = cityStatusSO.people.ToString();
            propertyText.text = cityStatusSO.propertyCount.ToString();
        }
    }
  
}
