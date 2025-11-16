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
        public int length;
        public VertexPath(int vertex, int length)
        {
            this.vertex = vertex;
            this.length = length;
            path = new List<int>();
        }
    }
    Vector2[] vertices;
    int[][] adjacencyMatrix;

    Graph(Vector2[] vertices)
    {
        this.vertices = vertices;
        adjacencyMatrix = Enumerable.Range(0, vertices.Length)
        .Select(_ => Enumerable.Repeat(-1, vertices.Length).ToArray())
        .ToArray();
        for (int i = 0; i < vertices.Length; i++)
        {
            adjacencyMatrix[i][i] = 0;
        }
    }

    public void AddEdge(int a, int b, int weight)
    {
        adjacencyMatrix[a][b]=weight;
        adjacencyMatrix[b][a]=weight;
    }

    public VertexPath Dijkstra(int from, int to)
    {
        //Setup
        VertexPath[] state = Enumerable.Range(0, vertices.Length).Select(i => new VertexPath(i, int.MaxValue)).ToArray(); // tableau (chemin, distance depuis origine)
        state[from].length = 0;
        List<int> todo = Enumerable.Range(0, this.vertices.Length).ToList();

        //Algorithm
        int current = from;
        while (todo.Count > 0)
        {
            UpdateNeighbours(state, current, adjacencyMatrix);

            todo.Remove(current);

            int min = int.MaxValue;
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

    void UpdateNeighbours(VertexPath[] state, int current, int[][] graph)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            if (graph[current][i] != -1 && graph[current][i] + state[current].length < state[i].length && i != current)
            {
                state[i].path = new List<int>(state[current].path)
                {
                    current
                };
                state[i].length = graph[current][i] + state[current].length;
            }
        }
    }

    
}
