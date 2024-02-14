using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Behave : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public int health = 100;
    public GameObject upgradePrefab;

    void Start()
    {
        InvokeRepeating("ShootBullets", 1f, 2f); // Invoke the ShootBullets method every 2 seconds after 1 second delay
    }

    void ShootBullets()
    {
        for (int i = 0; i < 360; i += 45)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, i);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = rotation * Vector2.up * bulletSpeed;
            Destroy(bullet, 6f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            int damage = 10;
            health -= damage;

            if (health <= 0)
            {
                Instantiate(upgradePrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            Destroy(other.gameObject);
        }
    }
}
