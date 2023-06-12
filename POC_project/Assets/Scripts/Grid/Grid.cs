using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid
{
    // grid variables
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    private Vector3 origin;

    // constructor
    public Grid(int width, int height, float cellSize, Vector3 origin)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;


        // initialize grid
        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                debugTextArray[x,z] = UtilsClass.CreateWorldText(gridArray[x, z].ToString(), null, GetWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * 0.5f, 10, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize + origin;
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z){
        x = Mathf.FloorToInt((worldPosition - origin).x / cellSize);
        z = Mathf.FloorToInt((worldPosition - origin).z / cellSize);
    }

    public void SetValue(int x, int z, int value){
        if(x >= 0 && z >= 0 && x < width && z < height){
        gridArray[x, z] = value;
        debugTextArray[x, z].text = gridArray[x, z].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value){
        int x, z;
        GetXZ(worldPosition, out x, out z);
        SetValue(x, z, value);
    }

    public void AddValue(int x, int z, int value){
        if(x >= 0 && z >= 0 && x < width && z < height){
        gridArray[x, z] += value;
        debugTextArray[x, z].text = gridArray[x, z].ToString();
        }
    }

    public bool IsWithinGrid(int x, int z){
        return (x >= 0 && z >= 0 && x < width && z < height);
    }
}
