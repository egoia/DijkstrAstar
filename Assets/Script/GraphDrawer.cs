using System.Collections.Generic;
using UnityEngine;
using static Graph;

public class GraphDrawer : MonoBehaviour
{

    public Vector2Int gridSize;
    public float edgeLength;
    Graph maze;

    public GameObject vertex;
    public GameObject edge;

    List<(GameObject, List<GameObject>)> graphObjects;


    public Graph graph;

    public void DrawGraph()
    {
        for(int i = 0; i<gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                GameObject vertexInstance = Instantiate(vertex, maze.vertices[i*gridSize.y + j], Quaternion.identity, transform);
                List<GameObject> edges = new List<GameObject>();
                foreach (var item in maze.edges[i*gridSize.y + j])
                {
                    GameObject edgeInstance = Instantiate(edge, transform);
                    edgeInstance.transform.localScale = new Vector3(edge.transform.lossyScale.x, item.Item2, edge.transform.lossyScale.y);
                    Vector2 linkedPosition = maze.vertices[item.Item1];

                    edgeInstance.transform.position = maze.vertices[i*gridSize.y + j] + (linkedPosition - maze.vertices[i*gridSize.y + j]).normalized * item.Item2;
                }
            }
        }
    }

    public void HighLightPath(VertexPath path)
    {
        
    }

}
