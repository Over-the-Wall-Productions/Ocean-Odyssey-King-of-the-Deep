using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    // reference to health
    private HealthController _healthController;

    // use this to get the reference
    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
    }

    public void startInvincibility(float duration)
    {
        StartCoroutine(invincibilityCoroutine(duration)); 
    }

    private IEnumerator invincibilityCoroutine(float duration)
    {
        _healthController.isInvincible = true;
        yield return new WaitForSeconds(duration);
        _healthController.isInvincible = false;
    }
}
