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
    Vector2[] vertices;
    List<(int, float)>[] edges;

    Graph(Vector2[] vertices, List<(int, float)>[] edges)
    {
        this.vertices = vertices;
       
        this.edges = edges;
    }

    public VertexPath Dijkstra(int from, int to)
    {
        //Setup
        VertexPath[] state = Enumerable.Range(0, vertices.Length).Select(i => new VertexPath(i, float.PositiveInfinity)).ToArray(); // tableau (chemin, distance depuis origine)
        state[from].length = 0;
        List<int> todo = Enumerable.Range(0, this.vertices.Length).ToList();

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
                if (state[vertex].length < min)
                {
                    min = state[vertex].length;
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

    public VertexPath Astar(int from, int to)
    {
        //Setup
        VertexPath[] state = Enumerable.Range(0, vertices.Length).Select(i => new VertexPath(i, float.PositiveInfinity)).ToArray(); // tableau (chemin, distance depuis origine)
        state[from].length = 0;
        List<int> todo = Enumerable.Range(0, this.vertices.Length).ToList();

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
                if (state[vertex].length + Vector2.Distance(vertices[vertex], vertices[from]) < min)
                {
                    min = state[vertex].length + Vector2.Distance(vertices[vertex], vertices[from]);
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

    
}
