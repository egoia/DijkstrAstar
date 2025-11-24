using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MazeGenerator
{
    public Vector2Int gridSize;
    public float edgeLength;
    public Graph maze;
    public Vector2Int originIndex;
    public void Init()
    {
        List<Vector2> vertices = new List<Vector2>(gridSize.x * gridSize.y);
        List<List<(int, float)>> edgesList = new List<List<(int, float)>>(gridSize.x * gridSize.y);
        for(int i = 0; i<gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                vertices.Add(new Vector2(i*edgeLength, j*edgeLength));
                List<(int, float)> edges = new List<(int, float)>();
                if (i == gridSize.x - 1)
                {
                    if(j!=gridSize.y-1){
                        edges.Add((i*gridSize.y +j+1, edgeLength));
                    }
                }
                else
                {
                    edges.Add(((i+1)*gridSize.y +j, edgeLength));
                }
                edgesList.Add(edges);

            }
        }
        maze = new Graph(vertices, edgesList.ToArray());
        originIndex = new Vector2Int(gridSize.x-1, gridSize.y-1);
    }

    public void OriginShift()
    {
        List<Vector2Int> neighboursIndexs = new List<Vector2Int>();
        if(originIndex.x+1<gridSize.x) neighboursIndexs.Add(new Vector2Int(originIndex.x+1, originIndex.y));
        if(originIndex.y+1<gridSize.y) neighboursIndexs.Add(new Vector2Int(originIndex.x, originIndex.y+1));
        if(originIndex.x-1>=0) neighboursIndexs.Add(new Vector2Int(originIndex.x-1, originIndex.y));
        if(originIndex.y-1>=0) neighboursIndexs.Add(new Vector2Int(originIndex.x, originIndex.y-1));
        Vector2Int nextOrigin = neighboursIndexs[Random.Range(0,neighboursIndexs.Count)];
        maze.edges[nextOrigin.x*gridSize.y + nextOrigin.y] = new List<(int, float)>();
        maze.edges[originIndex.x*gridSize.y + originIndex.y].Add((nextOrigin.x*gridSize.y + nextOrigin.y, edgeLength));
        originIndex = nextOrigin;
    }



    /*public void SaveMaze()
    {
        AssetDatabase.CreateAsset(maze, "Assets/Mazes/MyData.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = maze;
    }*/

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