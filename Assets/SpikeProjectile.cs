using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeProjectile : MonoBehaviour
{
    public float spikeDamage = 10;

    // This function is called when the renderer of the GameObject
    // becomes invisible to all cameras.
    private void OnBecameInvisible()
    {
        // Destroy the game object to free up resources
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            var playerHealth = collision.gameObject.GetComponent<HealthController>();
            playerHealth.TakeDamage(spikeDamage);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
