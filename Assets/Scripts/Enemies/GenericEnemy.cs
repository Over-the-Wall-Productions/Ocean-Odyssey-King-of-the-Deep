
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign your projectile prefab in the inspector
    public float shootingRate = 2f; // Time between shots
    public float projectileSpeed = 10f; // Speed of the projectile

    private float nextShootTime;

    // collision with urchin
    [SerializeField] private float _damageAmount;

    // health of urchin
    public int health = 20;

    // spike damage
    public float spikeDamage = 10;


    void Update()
    {
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
            Destroy(gameObject);
        }
    }


}
