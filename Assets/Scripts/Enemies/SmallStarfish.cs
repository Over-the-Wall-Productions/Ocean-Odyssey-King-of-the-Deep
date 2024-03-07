using UnityEngine;

public class starfishSmall : MonoBehaviour
{

    [SerializeField] private float _damageAmount;

    private float startTime;

    private bool priorTo = true;

    // health of starfish // takes 10 bullets
    public int health = 20;

    public Transform player; // Player's transform
    public float speed = 5.0f; // Speed at which the object moves towards the player
    public GameObject explosionEffectPrefab; // this is the invisible explosion that will spawn


    void Start()
    {
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

    //Add this function to handle collision with the bullet
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
            Destroy(collision.gameObject);
        }
    }

    // a method that keeps track of damage and health for urchin
    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
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
            Destroy(gameObject);
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
