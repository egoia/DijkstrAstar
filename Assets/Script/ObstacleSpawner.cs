using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public List<GameObject> obstacles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)];
        if(obstacle!=null)Instantiate(obstacle,transform);
    }

}
