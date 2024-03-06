using UnityEngine;
using System.Collections;

public class starfishShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign your projectile prefab in the inspector
    public float shootingRate = 2f; // Time between shots
    public float projectileSpeed = 10f; // Speed of the projectile
    public GameObject smallerStarfish;
    public GameObject explosionEffectPrefab;

    private float nextShootTime;

    // collision with urchin
    [SerializeField] private float _damageAmount;

    // health of starfish // takes 10 bullets
    public int health = 100;

    public float stopTime = 2f; // Time in seconds after which all forces should be stopped

    public Transform player; // Player's transform
    public float speed = 5.0f; // Speed at which the object moves towards the player

    private float startTime;

    private bool priorTo = true;

    void Start()
    {
        // Record the start time
        startTime = Time.time;
    }



    void Update()
    {
        if (priorTo && Time.time - startTime >= 2)
        {
            // Ensure this block runs only once
            priorTo = false; // Disable this script to stop checking

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // Stops linear movement
                rb.angularVelocity = 0f; // Stops rotational movement
            }

            StartFollowing();
        }

        if (player != null)
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // Calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }

        if (Time.time > nextShootTime)
        {
            ShootProjectile();
            nextShootTime = Time.time + shootingRate;
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab != null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 direction = (player.transform.position - transform.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }
            else
            {
                Debug.LogError("Projectile prefab does not have a Rigidbody2D component.");
            }
        }
    }

    // Add this function to handle collision with the bullet
    // used trigger becuase it prevents urchin from flying off after colliding with bullet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerBullet = collision.GetComponent<Bullet>();
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(playerBullet.damage);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("e_bullet"))
        {
            Score.scoreValue += 1;
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    // a method to drain player health on collisions
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            // get a reference to the player's health
            var playerHealth = collision.gameObject.GetComponent<HealthController>();

            playerHealth.TakeDamage(_damageAmount);
        }
    }

    // a method that keeps track of damage and health for urchin
    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void StartFollowing()
    {

        // Find the player by tag
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject != null)
        {
            player = playerGameObject.transform;
        }
    }

    private void Explode()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject instance = Instantiate(smallerStarfish, transform.position, Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply force in a random direction
                Vector2 forceDirection = Random.insideUnitCircle.normalized * 10f; // Adjust force magnitude as needed
                rb.AddForce(forceDirection, ForceMode2D.Impulse);
            }
        }
    }



}