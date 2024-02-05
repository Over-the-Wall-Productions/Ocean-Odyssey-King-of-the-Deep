
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign your projectile prefab in the inspector
    public float shootingRate = 2f; // Time between shots
    public float projectileSpeed = 10f; // Speed of the projectile

    private float nextShootTime;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // Ensure your bullet prefab has a tag "Bullet"
        {
            Destroy(gameObject); // Destroys this game object upon collision with a bullet
        }
    }
}
