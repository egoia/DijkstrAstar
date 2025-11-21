using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MazeGenerator
{
    public Vector2Int gridSize;
    public float edgeLength;
    Graph maze;

    int originIndex;

    public GameObject vertex;
    public GameObject edge;

    List<(GameObject, List<GameObject>)> graphObjects;

    public void Init()
    {
        List<Vector2> vertices = new List<Vector2>(gridSize.x * gridSize.y);
        List<List<(int, float)>> egdesList = new List<List<(int, float)>>(gridSize.x * gridSize.y);
        for(int i = 0; i<gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                vertices.Add(new Vector2(i*edgeLength, j*edgeLength));
                List<(int, float)> edges = new List<(int, float)>();
                if (i == gridSize.x - 1)
                {
                    if(j!=gridSize.y-1)edges.Add((i*gridSize.y *+j+1, edgeLength));
                }
                else
                {
                    edges.Add(((i+1)*gridSize.y *+j, edgeLength));
                }
                egdesList.Add(edges);

            }
        }
        maze = new Graph(vertices, egdesList.ToArray());
        originIndex = gridSize.x*gridSize.y;
    }

    public void Tick()
    {
        
    }


    Graph MakeItNonOriented(Graph graph)//TODO
    {
        return null;
    }
    public void SaveMaze()
    {
        AssetDatabase.CreateAsset(maze, "Assets/Mazes/MyData.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = maze;
    }

    List<Vector2Int> get4Neighbours(int x, int y)
    {
        List<Vector2Int> res = new List<Vector2Int>();
        if(x > 0) res.Add(new Vector2Int(x-1,y));
        if(x<gridSize.x)res.Add(new Vector2Int(x+1,y));
        if(y > 0) res.Add(new Vector2Int(x,y-1));
        if(y<gridSize.y)res.Add(new Vector2Int(x,y+1));
        return res;
    }


}