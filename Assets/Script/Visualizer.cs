using UnityEngine;

public class Visualizer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<GraphDrawer>().StartVisualization();
    }


}
