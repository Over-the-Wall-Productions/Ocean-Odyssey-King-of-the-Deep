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
        for (int i = Random.Range(0, 45); i < 360; i += 45)
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
            GameObject targetObject = GameObject.Find("Task Manager");
            if (targetObject != null)
            {
                // Get the TargetScript component attached to the GameObject
                LevelManager targetScript = targetObject.GetComponent<LevelManager>();

                if (targetScript != null)
                {
                    // Call the TargetFunction
                    targetScript.enemyManager();
                }
                else
                {
                    Debug.LogError("TargetScript not found on GameObject.");
                }
            }
            else
            {
                Debug.LogError("GameObject with name 'Task Manager' not found.");
            }
            Instantiate(upgradePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }

    }

}
