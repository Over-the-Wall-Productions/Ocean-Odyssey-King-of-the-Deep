using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject farEnemyPrefab;
    public GameObject closeEnemyPrefab;
    public float spawnInterval = 1.5f; // Time between spawns
    public float spawnRadius = 5f; // Minimum distance from the player

    private float nextSpawnTime;
    private Camera mainCamera;
    private GameObject player; // Reference to the player GameObject

    void Start()
    {
        mainCamera = Camera.main; // Cache the main camera
        player = GameObject.FindGameObjectWithTag("Player"); // Assuming player has the tag "Player"
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnPrefabInFreeArea(farEnemyPrefab);
            SpawnPrefabInFreeArea(closeEnemyPrefab);
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnPrefabInFreeArea(GameObject prefab)
    {
        Vector2 spawnPosition = GetRandomPositionOnScreen();

        // Check if the spawn position is within the specified radius from the player
        while (Vector2.Distance(spawnPosition, player.transform.position) < spawnRadius)
        {
            spawnPosition = GetRandomPositionOnScreen();
        }

        Instantiate(prefab, spawnPosition, Quaternion.identity);
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

