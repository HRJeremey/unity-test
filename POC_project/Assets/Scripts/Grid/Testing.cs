using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    private Grid grid;
    private int currentX;
    private int currentZ;
    private float timer;
    private const float timeThreshold = 5f;

    public GameObject player;
    private Stack<GridCell> logStack = new Stack<GridCell>();

    private void Start()
    {
        grid = new Grid(15, 15, 2f);
    }

    private void Update()
    {
        Vector3 playerWorldPosition = player.transform.position;

        grid.GetXZ(playerWorldPosition, out int x, out int z);

        if(grid.IsWithinGrid(x, z))
        {
            if(logStack.Count == 0)
            {
                logStack.Push(new GridCell(x,z));
                grid.AddValue(x,z,10);
                PrintStack();
            }
            if(x != currentX || z != currentZ){
                Debug.Log("Player has moved to a new grid cell");
                currentX = x;
                currentZ = z;
                timer = 0;
                logStack.Push(new GridCell(x,z));
                grid.AddValue(x,z,10);
                PrintStack();
            }
            else
            {
                // Player is still in the same grid cell
                timer += Time.deltaTime;

                if (timer >= timeThreshold)
                {
                    // Player has been in the same grid cell for more than 5 seconds
                    Debug.Log("Player has been in the same grid cell for more than 5 seconds");
                    grid.AddValue(x, z, 10);
                    logStack.Push(new GridCell(x,z));
                    timer = 0f;
                }
            }
        }
    }

    private void PrintStack()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var item in logStack)
        {
            sb.Append(item.ToString());
            sb.Append(" -> ");
        }

        string result = sb.ToString();
        Debug.Log(result);
    }
}

public class GridCell{
    public int x;
    public int z;

    public GridCell(int x, int z){
        this.x = x;
        this.z = z;
    }

    public override string ToString()
    {
        return "(" + x + "," + z + ")";
    }
}
