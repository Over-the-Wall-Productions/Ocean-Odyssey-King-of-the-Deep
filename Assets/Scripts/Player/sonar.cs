using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonar : MonoBehaviour
{
    /*
    public float growthTime = 3f; // Time in seconds until the object stops growing
    public float maxScale = 2f; // Maximum scale the object will reach
    public float initialTransparency = 1f; // Initial transparency of the object
    private float startTime;
    private Vector3 initialScale;
    private Renderer renderer;
    private Collider2D collider;
    public float pushForce = 10f; // The force to push away other objects

    void Start()
    {
        // Store the initial scale of the object
        initialScale = transform.localScale;

        // Record the start time
        startTime = Time.time;

        // Get the renderer component
        renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("Renderer component not found!");
            enabled = false; // Disable the script if renderer is not found
        }
        else
        {
            // Set the initial transparency
            SetTransparency(initialTransparency);
        }

        // Get the collider component
        collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("Collider component not found!");
            enabled = false; // Disable the script if collider is not found
        }
    }

    void Update()
    {
        // Calculate how much time has passed since the start
        float elapsedTime = Time.time - startTime;

        // Calculate the growth factor
        float growthFactor = Mathf.Clamp01(elapsedTime / growthTime);

        // Interpolate between initial scale and max scale using growth factor
        transform.localScale = Vector3.Lerp(initialScale, initialScale * maxScale, growthFactor);

        // Decrease transparency as the object grows
        float transparency = Mathf.Lerp(initialTransparency, 0f, growthFactor);
        SetTransparency(transparency);

        // Resize collider to match the size of the object
        //ResizeCollider();

        // Check if the transparency has reached zero
        if (transparency <= 0f)
        {
            // Destroy the object
            Destroy(gameObject);
        }
    }

    // Function to set transparency
    void SetTransparency(float alpha)
    {
        if (renderer != null)
        {
            Color color = renderer.material.color;
            color.a = alpha;
            renderer.material.color = color;
        }
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Calculate the direction away from the center of the colliding object
        Vector2 awayDirection = collision.contacts[0].point - (Vector2)transform.position;

        // Normalize the direction to get a unit vector
        awayDirection.Normalize();

        // Apply force to push away the colliding object
        Rigidbody2D otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRigidbody != null)
        {
            otherRigidbody.AddForce(awayDirection * pushForce, ForceMode2D.Impulse);
        }
    }
    */
}
