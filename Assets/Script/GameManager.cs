using System.Collections;
using UnityEngine;
using static Graph;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    GraphDrawer drawer;
    public bool astar;
    public float startWaitingTime;

    void OnEnable()
    {
        drawer = GetComponent<GraphDrawer>();
        drawer.DrawGameGraph();
        Debug.Log("generation done");
        VertexPath waypoints;
        if (astar) waypoints = drawer.maze.Astar(0,drawer.gridSize.x*drawer.gridSize.y-1);
        else waypoints = drawer.maze.Djikstra(0,drawer.gridSize.x*drawer.gridSize.y-1);
        drawer.HighLightPath(waypoints);
        Debug.Log("pathfinding done");
        player.GetComponent<Ball>().waypoints = drawer.maze.GetWayPoints(waypoints, transform);
        StartCoroutine("StartGameCoroutine");
        
    }

    IEnumerator StartGameCoroutine()
    {
        
        yield return StartCoroutine(Camera.main.GetComponent<CameraManager>().StartGameCoroutine());
        yield return new WaitForSeconds(startWaitingTime);
        player.GetComponent<Ball>().StartMoving();
        player.GetComponentInChildren<TrailRenderer>().emitting = true;
    }
}
