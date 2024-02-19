using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public Transform player; // Player's transform
    public float speed = 5.0f; // Speed at which the object moves towards the player

    [SerializeField] private float _damageAmount;

    void Start()
    {
        // Find the player by tag
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject != null)
        {
            player = playerGameObject.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // Calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
    }

    //Add this function to handle collision with the bullet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }



    //a method to drain player health on collision
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            var playerHealth = collision.GetComponent<HealthController>();
            playerHealth.TakeDamage(_damageAmount);
        }
    }
}
