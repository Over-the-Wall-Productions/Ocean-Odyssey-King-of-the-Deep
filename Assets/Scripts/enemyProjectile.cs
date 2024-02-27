using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    private Collider2D myCollider;
    public float destroyTime = 5f;
    private bool collided = false;

    private float _damageAmount = 10;

    void Start()
    {
        // Get the collider component
        myCollider = GetComponent<Collider2D>();
        if (myCollider == null)
        {
            Debug.LogError("Collider component not found!");
            enabled = false; // Disable the script if collider is not found
        }
        else
        {
            // Start the countdown for destruction
            Invoke("DestroyObject", destroyTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Set the collided flag to true
        collided = true;

        // Cancel the scheduled destruction
        CancelInvoke("DestroyObject");

        if (collision.GetComponent<Player>())
        {
            var playerHealth = collision.GetComponent<HealthController>();
            playerHealth.TakeDamage(_damageAmount);
        }

        // Destroy the object immediately
        Destroy(gameObject);
    }

    void DestroyObject()
    {
        // Check if the object has already collided
        if (!collided)
        {
            // Destroy the object
            Destroy(gameObject);
        }
    }
}
