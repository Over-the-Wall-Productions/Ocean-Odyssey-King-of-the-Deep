using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Behave : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;

    public int health = 10; // THIS IS SURE TO CHANGE (WE JUST HAVE THIS FOR THE DEMO)
    public int inkDamage = 25;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerBullet = collision.gameObject.GetComponent<Bullet>();
        if (playerBullet != null)
        {
            Damage(playerBullet.damage);
            Destroy(collision.gameObject);
        }

    }

    private void Damage(int damage)
    {
        health -= damage;
        if(health == 0)
        {
            Instantiate(upgradePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }

    }

}
