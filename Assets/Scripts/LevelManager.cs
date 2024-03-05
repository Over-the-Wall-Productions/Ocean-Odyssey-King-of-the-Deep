using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int gameOverScene;

    public int nextScene;

    private bool spawnersDone = false;

    public bool bossInScene = true;

    private bool bossDone = false;

    public TMP_Text taskText;

    public int spawnersLeft = 3;

    private int starsLeft = 9;

    private float startTime;

    private bool tasksStarted = false;

    public bool isStarLevel = false;

    public GameObject bossStarfish;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        GameObject targetObject = GameObject.Find("Main Player");
        if (targetObject == null)
        {
            GameOver();
        }
        
        if (!tasksStarted && Time.time - startTime > 5) {
            taskUpdater();
            tasksStarted = true;
        }

        if (bossDone && spawnersDone){
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(gameOverScene);
    }

    public void enemyManager()
    {
        if (!spawnersDone)
        {
            --spawnersLeft;
            if (spawnersLeft == 0)
            {
                spawnersDone = true;

                if (!bossInScene)
                {
                    bossDone = true;
                }

                if (isStarLevel)
                {
                    Vector3 spawnPosition = new Vector3(95, -85, 0);

                    // Instantiate the object at the specified position
                    Instantiate(bossStarfish, spawnPosition, Quaternion.identity);
                }
            }
            taskUpdater();
        }
        else if (isStarLevel)
        {
            --starsLeft;
            if (starsLeft == 0)
            {
                bossDone = true;
            }
            taskUpdater();
        }
        else if (bossInScene && !bossDone)
        {
            bossDone = true;
            taskUpdater();
        }
    }

    private void taskUpdater()
    {
        if (!spawnersDone) {
           taskText.text = "Spawners Left: " + spawnersLeft;
        }
        else if (isStarLevel && !bossDone)
        {
            double percentage = ((9 - starsLeft) / 9) * 1000;

            taskText.text = "There's a dangerous creature near you. Defeat It: " + percentage.ToString("F2") + "%";
        }
        else if (!bossDone)
        {
            taskText.text = "There's a dangerous creature near you. Defeat It";
        }
        else
        {
            taskText.text = "Great Job! Press Enter for extraction";
        }
    }
}
