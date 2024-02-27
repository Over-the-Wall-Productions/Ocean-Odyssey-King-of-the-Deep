using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigi : MonoBehaviour
{
    public float growthTime = 3f; // Time in seconds until the object stops growing
    public float maxScale = 2f; // Maximum scale the object will reach

    private Vector3 initialScale;
    private float startTime;

    void Start()
    {
        // Store the initial scale of the object
        initialScale = transform.localScale;

        // Record the start time
        startTime = Time.time;
    }

    void Update()
    {
        // Calculate how much time has passed since the start
        float elapsedTime = Time.time - startTime;

        // Calculate the growth factor
        float growthFactor = Mathf.Clamp01(elapsedTime / growthTime);

        // Interpolate between initial scale and max scale using growth factor
        transform.localScale = Vector3.Lerp(initialScale, initialScale * maxScale, growthFactor);

        // Check if the object has reached its maximum scale
        if (growthFactor >= 1f)
        {
            // Destroy the object
            Destroy(gameObject);
        }
    }
}
