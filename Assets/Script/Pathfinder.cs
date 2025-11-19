using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public struct Edge
    {
        Transform A;
        Transform B;
        int length;
    }
    public Transform[] vertices;
    public Edge[] edges;

    Graph graph;


    void Start()
    {
        

    }
}