using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Graph;

public class GraphDrawer : MonoBehaviour
{

    public Vector2Int gridSize;
    public Graph maze;
    public GameObject vertex;
    public GameObject edge;
    List<(GameObject, List<(int,GameObject)>)> graphObjects;
    public float edgeLength = 5f;
    public MazeGenerator mazeGenerator;

    public float timeBetweenUpdate = 0.05f;

    void OnEnable()
    {
        InitTest();
        GenerateMaze();
        //StartCoroutine("VisualizeOriginShiftCoroutine");
        Camera.main.orthographicSize = (edgeLength*gridSize.y +5)/2f;
    }

    [ContextMenu("regenerate")]
    void TestPathfind()
    {
        Clear();
        InitTest();
        GenerateMaze();
    }

    void GenerateMaze()
    {
        for (int i = 0; i < gridSize.x*gridSize.y*10; i++)
        {
            mazeGenerator.OriginShift();
        }
        MakeItNonOriented(maze);
        DrawGraph(); 
        HighLightPath(maze.Astar(0, gridSize.x*gridSize.y -1));
    }

    IEnumerator VisualizeOriginShiftCoroutine()
    {
        //visualize origin shift algorithm
        for (int i = 0; i < gridSize.x*gridSize.y*10; i++)
        {
            Tick();
            yield return new WaitForSeconds(timeBetweenUpdate);
        }
    }

    [ContextMenu("Init test")]
    public void InitTest()
    {
        mazeGenerator = new MazeGenerator();
        mazeGenerator.gridSize = gridSize;
        mazeGenerator.edgeLength = edgeLength;
        mazeGenerator.Init();
        maze = mazeGenerator.maze;
    }

    [ContextMenu("Draw")]
    public void DrawGraph()
    {
        transform.position = Vector2.zero;

        graphObjects = new List<(GameObject, List<(int,GameObject)>)>();
        for(int i = 0; i<gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                List<(int,GameObject)> edges = new List<(int,GameObject)>();
                foreach (var item in maze.edges[i*gridSize.y + j])
                {
                    if(i*gridSize.y + j > item.Item1)
                    {
                        GameObject edgeInstance = Instantiate(edge, transform);
                        edgeInstance.transform.localScale = new Vector3(item.Item2, edge.transform.lossyScale.y, edge.transform.lossyScale.y);
                        Vector2 linkedPosition = maze.vertices[item.Item1];

                        edgeInstance.transform.position = maze.vertices[i*gridSize.y + j] + (linkedPosition - maze.vertices[i*gridSize.y + j]).normalized * item.Item2/2;
                        edgeInstance.transform.rotation = linkedPosition.y==maze.vertices[i*gridSize.y + j].y ? Quaternion.Euler(0,0,0) : Quaternion.Euler(0,0,90);
                        edges.Add((item.Item1,edgeInstance));
                    }
                }
                GameObject vertexInstance = Instantiate(vertex, maze.vertices[i*gridSize.y + j], Quaternion.identity, transform);
                graphObjects.Add((vertexInstance, edges));
            }
        }
        transform.position = new Vector2(-gridSize.x/2 * edgeLength, -gridSize.y/2 * edgeLength);

        //HighlightOrigin();
    }

    [ContextMenu("OriginShift")]
    void Tick()
    {
        mazeGenerator.OriginShift();
        Clear();
        DrawGraph();
    }

    void Clear()
    {
        foreach (var vertex in graphObjects)
        {
            Destroy(vertex.Item1);
            foreach (var edge in vertex.Item2)
            {
                Destroy(edge.Item2);
            }
        }
    }

    public void HighLightPath(VertexPath path)
    {
        for(int i =0; i<path.path.Count; i++)
        {
            graphObjects[path.path[i]].Item1.GetComponent<Highlighter>().Highlight(true);
            foreach(var edge in graphObjects[path.path[i]].Item2)
            {
                if(i != path.path.Count - 1 && edge.Item1 == path.path[i+1])edge.Item2.GetComponent<Highlighter>().Highlight(true);
                if(i != 0 && edge.Item1 == path.path[i-1])edge.Item2.GetComponent<Highlighter>().Highlight(true);
            }
        }
    }

    public void HighlightOrigin()
    {
        Vector2Int originIndex = mazeGenerator.originIndex;
        graphObjects[originIndex.x*gridSize.y + originIndex.y].Item1.GetComponent<Highlighter>().Highlight(true);
    }

}
