using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform bulletSpawn;

    public GameObject bulletPrefab;

    public float force = 20f;

    public float shotDelay = 1f;
    public float timeOfLastShot;

    private void Update()
    {
        // Fire1 is mousepad for keyboard
        if(Input.GetButtonDown("Fire1"))
        {
            // Delay between shots
            if (Time.time - timeOfLastShot >= shotDelay)
            {
                Shoot();
                timeOfLastShot = Time.time;
            }   
            
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
