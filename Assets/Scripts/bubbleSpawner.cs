using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleSpawner : MonoBehaviour
{
    public GameObject bubble; // The prefab to instantiate
    public float spawnInterval = 1f; // Time interval between spawns
    private Camera mainCamera;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;

        // Calculate the size of the object
        CalculateObjectSize();

        // Check if the object to instantiate is provided
        if (bubble == null)
        {
            Debug.LogError("Prefab to instantiate is not assigned!");
            return;
        }

        // Start spawning objects
        StartCoroutine(SpawnObjects());
    }

    void CalculateObjectSize()
    {
        // Get the sprite renderer component if available
        SpriteRenderer spriteRenderer = bubble.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Get the width and height of the sprite
            objectWidth = spriteRenderer.bounds.size.x;
            objectHeight = spriteRenderer.bounds.size.y;
        }
        else
        {
            // Get the collider component if sprite renderer is not available
            Collider2D collider = bubble.GetComponent<Collider2D>();
            if (collider != null)
            {
                // Get the size of the collider
                objectWidth = collider.bounds.size.x;
                objectHeight = collider.bounds.size.y;
            }
            else
            {
                // Use default size if both sprite renderer and collider are not available
                objectWidth = 1f;
                objectHeight = 1f;
            }
        }
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Instantiate the object within the camera bounds
            InstantiateObjectWithinBounds();

            // Wait for the specified spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void InstantiateObjectWithinBounds()
    {
        // Calculate the camera bounds
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calculate the position range within camera bounds, taking into account the camera's current position
        float xRange = cameraWidth / 2f - objectWidth / 2f;
        float yRange = cameraHeight / 2f - objectHeight / 2f;

        // Generate random position within camera bounds, relative to the camera's position
        float randomX = Random.Range(-xRange, xRange) + mainCamera.transform.position.x;
        float randomY = Random.Range(-yRange, yRange) + mainCamera.transform.position.y;

        // Instantiate the object at the random position
        Instantiate(bubble, new Vector3(randomX, randomY, 0f), Quaternion.identity);
    }
}
