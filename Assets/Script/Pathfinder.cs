using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Pathfinder : MonoBehaviour
{

    public GameObject ball;
    public GraphDrawer drawer;
    void Start()
    {
        
        ball.GetComponent<Ball>().waypoints = drawer.maze.GetWayPoints(drawer.maze.Astar(0,drawer.gridSize.x*drawer.gridSize.y-1),drawer.transform);
    }

}