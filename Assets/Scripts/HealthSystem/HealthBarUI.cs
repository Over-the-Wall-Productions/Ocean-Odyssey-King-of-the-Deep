using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image _healthBarForeground;

    public void UpdateHealthBar(HealthController healthController)
    {
        _healthBarForeground.fillAmount = healthController.RemainingHealthPercentage;
    }
}
