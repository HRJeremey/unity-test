using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    private Grid grid;
    public GameObject player;
    private Stack<string> logStack = new Stack<string>();

    private void Start()
    {
        grid = new Grid(5, 5, 2f);
    }

    private void Update()
    {
        Vector3 playerWorldPosition = player.transform.position;

        grid.GetXZ(playerWorldPosition, out int x, out int z);

        if(logStack.Count == 0)
        {
            logStack.Push(x.ToString() + ", " + z.ToString());
            PrintStack();
        }
        if(grid.IsWithinGrid(x, z)){
            if(logStack.Peek() != x.ToString() + ", " + z.ToString())
            {
                logStack.Push(x.ToString() + ", " + z.ToString());
                PrintStack();
            }
            grid.SetValue(x, z, 10);
        }
    }

    private void PrintStack()
    {
        StringBuilder sb = new StringBuilder();

        foreach (string item in logStack)
        {
            sb.Append(item);
            sb.Append(" -> ");
        }

        string result = sb.ToString();
        Debug.Log(result);
    }
}
