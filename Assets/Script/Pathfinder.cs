using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Pathfinder : MonoBehaviour
{

    public struct Vertex
    {
        public GameObject A;
        public List<Edge> edges;
    }
    public struct Edge
    {
        public GameObject link;
        public Vertex B;
        public float length;
    }
    List<Vertex> graphObjects;

    Graph graph;


    void Start()
    {
        graph = ToGraph();
        List<int> path = graph.Astar(0,graphObjects.Count-1).path;

    }


    Graph ToGraph()
    {
        List<(int,float)>[] graphEdges = Enumerable.Repeat(new List<(int,float)>(), graphObjects.Count).ToArray();
        for (int i = 0; i < graphEdges.Length; i++)
        {
            graphEdges[i] = graphObjects[i].edges.Select(e => (graphObjects.IndexOf(e.B), e.length)).ToList();
        }

        List<Vector2> vertices = graphObjects.Select(i => (Vector2)i.A.transform.position).ToList();
        return new Graph(vertices, graphEdges);
    }

}