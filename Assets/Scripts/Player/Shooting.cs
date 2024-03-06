using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooting : MonoBehaviour
{
    public Transform bulletSpawn;

    public GameObject bulletPrefab;

    public GameObject explosivePrefab;

    public float force = 20f;

    private float shotDelay = 0.5f;
    public float timeOfLastShot;

    public float explodeDelay = 2f;
    public float timeOfLastbomb;

    public UnityEvent<float> onReload;

    private bool canShoot = true; //new
    private bool explode = true;
    public bool equipped = true;

    [SerializeField] private AudioSource shootSound;



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

        if (!explode)
        {
            float currentDelay = explodeDelay - (Time.time - timeOfLastbomb);
            onReload?.Invoke(currentDelay);

            if (currentDelay <= 0)
            {
                explode = true;
            }
        }

        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot();
            shootSound.Play();
            timeOfLastShot = Time.time;
            canShoot = false;
        }
        if (Input.GetButtonDown("Fire2") && explode && equipped)
        {
            Shoot_Explosive();
            shootSound.Play();
            timeOfLastbomb = Time.time;
            explode = false;

        }
    }

    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // impulse adds foce impulse to RigidBody2D
        rb.AddForce(bulletSpawn.up * force, ForceMode2D.Impulse);
    }

    // the script is the same as shoot but it uses a different arrow that has different behavior
    private void Shoot_Explosive()
    {
        var bullet = Instantiate(explosivePrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // impulse adds foce impulse to RigidBody2D
        rb.AddForce(bulletSpawn.up * force, ForceMode2D.Impulse);
    }

    //private void Sonar()
    //{
    //    // Calculate the center of the current object
    //    Vector3 center = transform.position;

    //    // Instantiate the object at the center of the current object
    //    Instantiate(sonarPrefab, center, Quaternion.identity);
    //}
    //using System.Collections;
    //using System.Collections.Generic;
    //using UnityEngine;
    //using UnityEngine.Events;

    //public class Shooting : MonoBehaviour
    //{
    //    public Transform bulletSpawn;

    //    public GameObject bulletPrefab;

    //<<<<<<< Updated upstream
    //=======
    //    public GameObject explosivePrefab;
    //>>>>>>> Stashed changes

    //    public float force = 20f;

    //    private float shotDelay = 0.5f;
    //    public float timeOfLastShot;

    //<<<<<<< Updated upstream
    //    public float timeOfLastSonar;
    //=======
    //    public float explodeDelay = 1f;
    //    public float timeOfLastbomb;
    //>>>>>>> Stashed changes

    //    public UnityEvent<float> onReload;

    //    private bool canShoot = true; //new
    //    private bool explode = true;
    //    private bool equipped = true;

    //    [SerializeField] private AudioSource shootSound;



    //    private void Update()
    //    {

    //        if (!canShoot)
    //        {
    //            float currentDelay = shotDelay - (Time.time - timeOfLastShot);
    //            onReload?.Invoke(currentDelay);

    //            if (currentDelay <= 0)
    //            {
    //                canShoot = true;
    //            }
    //        }

    //        if (!explode)
    //        {
    //            float currentDelay = explodeDelay - (Time.time - timeOfLastbomb);
    //            onReload?.Invoke(currentDelay);

    //            if (currentDelay <= 0)
    //            {
    //                explode = true;
    //            }
    //        }

    //        if (Input.GetButtonDown("Fire1") && canShoot)
    //        {
    //            Shoot();
    //            shootSound.Play();
    //            timeOfLastShot = Time.time;
    //            canShoot = false;
    //        }
    //<<<<<<< Updated upstream
    //=======
    //        if (Input.GetButtonDown("Fire2") && explode && equipped)
    //        {
    //            Shoot_Explosive();
    //            shootSound.Play();
    //            timeOfLastbomb = Time.time;
    //            explode = false;
    //>>>>>>> Stashed changes

    //    }

    //    private void Shoot()
    //    {
    //        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    //        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

    //        // impulse adds foce impulse to RigidBody2D
    //        rb.AddForce(bulletSpawn.up * force, ForceMode2D.Impulse);
    //    }

    //    // the script is the same as shoot but it uses a different arrow that has different behavior
    //    private void Shoot_Explosive()
    //    {
    //        var bullet = Instantiate(explosivePrefab, bulletSpawn.position, bulletSpawn.rotation);
    //        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

    //<<<<<<< Updated upstream
    //        // Instantiate the object at the center of the current object
    //        //Instantiate(sonarPrefab, center, Quaternion.identity);
    //=======
    //        // impulse adds foce impulse to RigidBody2D
    //        rb.AddForce(bulletSpawn.up * force, ForceMode2D.Impulse);
    //>>>>>>> Stashed changes
    //    }

    //    //private void Sonar()
    //    //{
    //    //    // Calculate the center of the current object
    //    //    Vector3 center = transform.position;

    //    //    // Instantiate the object at the center of the current object
    //    //    Instantiate(sonarPrefab, center, Quaternion.identity);
    //    //}
    //}
}
