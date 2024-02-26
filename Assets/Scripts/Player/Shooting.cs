using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooting : MonoBehaviour
{
    public Transform bulletSpawn;

    public GameObject bulletPrefab;

    public float force = 20f;

    private float shotDelay = 0.5f;
    public float timeOfLastShot;

    public UnityEvent<float> onReload;

    private bool canShoot = true; //new

    private void Update()
    {
        
        if (!canShoot)
        {
            float currentDelay = shotDelay - (Time.time - timeOfLastShot);
            onReload?.Invoke(currentDelay);

            if (currentDelay <= 0)
            {
                canShoot = true;
            }
        }

        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot();
            timeOfLastShot = Time.time;
            canShoot = false;
        }
    }

    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // impulse adds foce impulse to RigidBody2D
        rb.AddForce(bulletSpawn.up * force, ForceMode2D.Impulse);
    }
}
