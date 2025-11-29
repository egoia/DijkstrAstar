using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Graph
{
    public class VertexPath
    {
        public int vertex;
        public List<int> path;
        public float length;
        public VertexPath(int vertex, float length)
        {
            this.vertex = vertex;
            this.length = length;
            path = new List<int>();
        }
    }
    public List<Vector2> vertices;
    public List<(int, float)>[] edges;

    public Graph(List<Vector2> vertices, List<(int, float)>[] edges)
    {

        this.vertices = vertices;
        this.edges = edges;
    }

    public VertexPath Astar(int from, int to)
    {
        return Pathfinding(from,to,AstarDistance);
    }

    public VertexPath Djikstra(int from, int to)
    {
        return Pathfinding(from,to,DijkstraDistance);
    }

    public VertexPath Pathfinding(int from, int to, Func<int, VertexPath[], int, float> weightFunction)
    {
        //Setup
        VertexPath[] state = Enumerable.Range(0, vertices.Count).Select(i => new VertexPath(i, float.PositiveInfinity)).ToArray(); // tableau (chemin, distance depuis origine)
        state[from].length = 0;
        List<int> todo = Enumerable.Range(0, this.vertices.Count).ToList();

        //Algorithm
        int current = from;
        while (todo.Count > 0)
        {
            UpdateNeighbours(state, current, edges);

            todo.Remove(current);

            float min = float.PositiveInfinity;
            int next = -1;
            foreach(var vertex in todo)
            {
                if (weightFunction(from, state, vertex) < min)
                {
                    min = weightFunction(from, state, vertex);
                    next = vertex;
                }
            }
            if(next==-1) return state[to]; // graphe non connexe
            if (next == to)//fin
            {
                state[to].path.Add(to);
                return state[to]; 
            }

            current = next;
        }
        return state[to];

    }

    private float DijkstraDistance(int from, VertexPath[] state, int vertex)
    {
        return state[vertex].length;
    }


    private float AstarDistance(int from, VertexPath[] state, int vertex)
    {
        return state[vertex].length + Vector2.Distance(vertices[vertex], vertices[from]);
    }

    void UpdateNeighbours(VertexPath[] state, int current, List<(int, float)>[] edges)
    {

        foreach (var edge in edges[current])
        {
           if(edge.Item2 + state[current].length < state[edge.Item1].length)
            {
                state[edge.Item1].path = new List<int>(state[current].path)
                {
                    current
                };
                state[edge.Item1].length = edge.Item2 + state[current].length;
            } 
        }
    }

    public static void MakeItNonOriented(Graph graph)
    {
        List<List<(int, float)>> nonOrientedEdges = new List<List<(int, float)>>();
        for(int i = 0; i<graph.vertices.Count; i++)
        {
            nonOrientedEdges.Add(new List<(int, float)>());
        }
        for(int i = 0; i<graph.vertices.Count; i++)
        {
            foreach (var edge in graph.edges[i])
            {
                nonOrientedEdges[i].Add(edge);
                nonOrientedEdges[edge.Item1].Add((i, edge.Item2));
            }
        }
        graph.edges = nonOrientedEdges.ToArray();
    }

    public static Graph Optimize(Graph graph)//TODO
    {
        return null;
    }

    public Vector2[] GetWayPoints(VertexPath path, Transform parent)
    {
        List<Vector2> waypoints = new List<Vector2>();
        foreach (var point in path.path)
        {
            waypoints.Add(vertices[point]+(Vector2)parent.position);
        }
        return waypoints.ToArray();
    }
    
}
