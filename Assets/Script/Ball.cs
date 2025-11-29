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
    bool moving = false;
    public float closeRange = 0.1f;
    public float dashAdditionnalSpeed;
    public float dashTime;
    public bool isDashing {get;private set;}
    float dashTimer = 0;



    void Start()
    {
        isDashing = false;
        index = 1;
        transform.position = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            dashTimer+=Time.deltaTime;
            if (dashTimer >= dashTime)
            {
                isDashing = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            dashTimer = 0;
        }
        Debug.Log("aqui");

        if (moving)
        {
            
        Debug.Log("ici");
            float currentSpeed = speed;
            if(isDashing)currentSpeed+=dashAdditionnalSpeed;
            transform.position = Vector2.MoveTowards(transform.position, waypoints[index], currentSpeed*Time.deltaTime);
            if(Vector2.Distance(transform.position, waypoints[index]) <= closeRange)
            {
                transform.position = waypoints[index];
                index++;
                if(index>= waypoints.Length)moving = false;
            }
        }
    }

    public void resetDash()
    {
        isDashing = false;
    }

    public void StartMoving()
    {
        moving=true;
    }
}
