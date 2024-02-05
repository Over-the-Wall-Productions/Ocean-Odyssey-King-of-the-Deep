using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnInterval = 2f; // Time between spawns

    private float nextSpawnTime;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Cache the main camera
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnPrefabInFreeArea();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnPrefabInFreeArea()
    {
        Vector2 spawnPosition = GetRandomPositionOnScreen();
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

    Vector2 GetRandomPositionOnScreen()
    {
        // Get the screen bounds
        float screenX = Screen.width;
        float screenY = Screen.height;

        // Convert screen bounds to world space
        Vector2 lowerLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector2 upperRight = mainCamera.ScreenToWorldPoint(new Vector3(screenX, screenY, mainCamera.nearClipPlane));

        // Generate a random position within the screen bounds
        float randomX = Random.Range(lowerLeft.x, upperRight.x);
        float randomY = Random.Range(lowerLeft.y, upperRight.y);

        return new Vector2(randomX, randomY);
    }
}
