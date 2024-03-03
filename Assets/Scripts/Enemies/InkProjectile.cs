using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkProjectile : MonoBehaviour
{
    public float damage = 25;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            var playerHealth = collision.gameObject.GetComponent<HealthController>();
            playerHealth.TakeDamage(damage);
        }
    }
}
