using System.Collections;
using UnityEngine;

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
        Vector2[] waypoints;
        if (astar) waypoints = drawer.maze.GetWayPoints(drawer.maze.Astar(0,drawer.gridSize.x*drawer.gridSize.y-1),drawer.transform);
        else waypoints = drawer.maze.GetWayPoints(drawer.maze.Djikstra(0,drawer.gridSize.x*drawer.gridSize.y-1),drawer.transform);
        Debug.Log("pathfinding done");
        player.GetComponent<Ball>().waypoints = waypoints;
        StartCoroutine("StartGameCoroutine");
        
    }

    IEnumerator StartGameCoroutine()
    {
        
        yield return StartCoroutine(Camera.main.GetComponent<CameraManager>().StartGameCoroutine());
        yield return new WaitForSeconds(startWaitingTime);
        player.GetComponent<Ball>().StartMoving();
    }
}
