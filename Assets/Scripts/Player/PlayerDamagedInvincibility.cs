using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedInvincibility : MonoBehaviour
{
    private InvincibilityController _invincibilityController;

    [SerializeField] private float _duration;

    private void Awake()
    {
        _invincibilityController = GetComponent<InvincibilityController>();
    }

    public void startInvincibility()
    {
        _invincibilityController.startInvincibility(_duration);
    }
}
