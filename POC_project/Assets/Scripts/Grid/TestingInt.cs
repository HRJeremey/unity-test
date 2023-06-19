using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class TestingInt : MonoBehaviour
{
    private GenericGrid grid;
    private int currentX;
    private int currentZ;
    private float timer;
    private const float timeThreshold = 5f;

    public GameObject player;
    private Stack<GridCell> logStack = new Stack<GridCell>();
    private Stack<int> distanceStack = new Stack<int>();

    public int width;
    public int height;
    public int cellSize;
    public Vector3 origin;
    public Sprite imageSprite;
    public GameObject target;

    private void Start()
    {
        grid = new GenericGrid(width, height, cellSize, origin, imageSprite);
    }

    private void Update()
    {
        Vector3 playerWorldPosition = player.transform.position;

        grid.GetXZ(playerWorldPosition, out int x, out int z);

        if (grid.IsWithinGrid(x, z))
        {
            if (logStack.Count == 0)
            {
                UpdateGrid(playerWorldPosition, x, z);
                StartCoroutine(FetchLocation(player));

            }
            if (x != currentX || z != currentZ)
            {
                Debug.Log("Player has moved to a new grid cell");
                currentX = x;
                currentZ = z;
                timer = 0;
                UpdateGrid(playerWorldPosition, x, z);
                StartCoroutine(FetchLocation(player));
            }
            else
            {
                // Player is still in the same grid cell
                timer += Time.deltaTime;

                if (timer >= timeThreshold)
                {
                    // Player has been in the same grid cell for more than 5 seconds
                    Debug.Log("Player has been in the same grid cell for more than 5 seconds");
                    UpdateGrid(playerWorldPosition, x, z);
                    StartCoroutine(FetchLocation(player));
                    timer = 0f;
                }
            }
        }
    }

    private void UpdateGrid(Vector3 playerWorldPosition, int x, int z)
    {
        logStack.Push(new GridCell(x, z));
        distanceStack.Push((int)Vector3.Distance(target.transform.position, playerWorldPosition));
        var currentValue = grid.GetValue(playerWorldPosition);
        grid.SetValue(x, z, currentValue += 5);
        PrintStack();
    }

    private IEnumerator FetchLocation(GameObject player)
    {
        List<int> distanceList = distanceStack.ToList();
        int newDistance = distanceList[0];
        int currentDistance = distanceList.Count >= 2 ? distanceList[1] : distanceList[0];
        Debug.Log($"current distance = {currentDistance} new distance = {newDistance}");
        string url = $"http://api.hyperionar.stroetenga.nl/Calculations/DoseRateAtNewDistance?DoseRate=12&CurrentDistance={currentDistance}&NewDistance={newDistance}";
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest.downloadHandler.text);

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

public class GridCell
{
    public int x;
    public int z;

    public GridCell(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public override string ToString()
    {
        return "(" + x + "," + z + ")";
    }
}
