using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Conditions : MonoBehaviour
{
    // short script that checks if the win conditions for the respective level have been met in order to destroy the barrier
    public float condo_count;
    void Update()
    {
        condo_count = CountObjectsWithTag("Spawner_box");
        if (condo_count <= 0)
        {
            Destroy(gameObject);
        }
    }
    int CountObjectsWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        return objectsWithTag.Length;
    }
    public void decrease()
    {
        condo_count -= 1;
    }
}
