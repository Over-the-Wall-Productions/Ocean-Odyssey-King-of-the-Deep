using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public Transform player; // Player's transform
    public float speed = 5.0f; // Speed at which the object moves towards the player

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

    // Add this function to handle collision with the bullet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // Ensure your bullet prefab has a tag "Bullet"
        {
            Destroy(gameObject); // Destroys this game object upon collision with a bullet
        }
    }
}
