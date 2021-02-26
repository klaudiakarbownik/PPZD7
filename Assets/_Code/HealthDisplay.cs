using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {
    public Image healthFillUI;

    public void SetHealthValue(float value) {
        healthFillUI.fillAmount = value;
    }
}