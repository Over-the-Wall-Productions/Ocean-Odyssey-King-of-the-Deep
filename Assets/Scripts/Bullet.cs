using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    public int damage = 2;

    // Variable to hold the rotation offset value
    public float rotationOffset = 0f;

    private void Start()
    {
        // Get the Rigidbody2D component attached to the GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Update the GameObject's rotation based on its velocity
        if (rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            // Apply the rotation offset to the calculated angle
            transform.rotation = Quaternion.AngleAxis(angle + rotationOffset, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sea Urchin") | collision.gameObject.CompareTag("Puffer Fish") | collision.gameObject.CompareTag("Spike"))
        {
            Destroy(gameObject);

        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Sea Urchin") | collision.CompareTag("Puffer Fish") | collision.CompareTag("Spike"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
