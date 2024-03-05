using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning_boxes : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies = 1; // The number of enemies spawning at one time
    // public int max_enemies = 20 // need to implement this along with edits to enemies
    public float spawn_interval = 2.0f; 
    public Transform spawnBox;  
    private float next_spawn_time = 0f;
    //private bool alive = true;
    public float box_health = 4f;

    //Boss_Conditions bc;

    void Start()
    {

        next_spawn_time = Time.time + spawn_interval;
    }

    void Update()
    {
        if (Time.time >= next_spawn_time)
        {
            SpawnEnemy();
            next_spawn_time = Time.time + spawn_interval;

        }
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Generate a random position above the spawning box
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnBox.position.x - spawnBox.localScale.x / 2f, spawnBox.position.x + spawnBox.localScale.x / 2f),
                spawnBox.position.y + spawnBox.localScale.y / 2.5f,
               1
                //Random.Range(spawnBox.position.z - spawnBox.localScale.z / 2f, spawnBox.position.z + spawnBox.localScale.z / 2f)
            );

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Bullet"))
    //    {
    //        box_health -= 1;
    //        Destroy(collision.gameObject);
    //    }
    //    if (box_health <= 0)
    //    {
    //        var component = GetComponent<Boss_Conditions>();
    //        component.condo_count -= 1;
    //        //bc.decrease();
    //        //Destroy(gameObject);
    //        Invoke("remove", 1f);
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            box_health -= 1;
            Destroy(collision.gameObject);
        }
        if (box_health <= 0)
        {
            //var component = GetComponent<Boss_Conditions>();
            //component.condo_count -= 1;
            //bc.decrease();
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

            //Invoke("remove", 1f);
        }
    }

    private void remove()
    {
        Destroy(gameObject);
    }

}
