using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    [HideInInspector] public Vector2[] waypoints;
    int index = -1;
    public float speed;
    bool moving = true;
    public float closeRange = 0.1f;


    void Start()
    {
        index = 1;
        transform.position = waypoints[0];
        foreach (var item in waypoints)Debug.Log(item);
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[index], speed*Time.deltaTime);
            if(Vector2.Distance(transform.position, waypoints[index]) <= closeRange)
            {
                transform.position = waypoints[index];
                index++;
                if(index>= waypoints.Length)moving = false;
            }
        }
    }
}
