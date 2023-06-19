using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenericGrid
{
    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 0;

    //public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;

    // grid variables
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    private Image[,] debugTextArrayPro;
    private Vector3 origin;
    private readonly Sprite sprite;

    // constructor
    public GenericGrid(int width, int height, float cellSize, Vector3 origin, Sprite sprite = null)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;
        this.sprite = sprite;

        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        canvasRectTransform.sizeDelta = new Vector2(width * cellSize, height * cellSize);
        canvasRectTransform.position = origin + new Vector3(width * cellSize, 1f, height * cellSize) * 0.5f;

        // Calculate the center position of the grid
        Vector3 gridCenter = origin + new Vector3(width * cellSize, 0f, height * cellSize) * 0.5f;

        // Set the position of the Canvas to align with the grid on the floor
        canvasRectTransform.position = gridCenter;

        // Adjust the Canvas position to be at the same depth as the grid (floor)
        Vector3 canvasPosition = canvasRectTransform.position;
        canvasPosition.y = origin.y;
        canvasRectTransform.position = canvasPosition;
        canvasRectTransform.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        // initialize grid
        gridArray = new int[width, height];
        debugTextArrayPro = new Image[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                // create text mesh pro text on each grid
                CreateAndSetTextMeshProperties(cellSize, canvas, x, z);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, Time.deltaTime, false);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, Time.deltaTime, false);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, Time.deltaTime, false);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, Time.deltaTime, false);
    }

    private void CreateAndSetTextMeshProperties(float cellSize, Canvas canvas, int x, int z)
    {
        var image = debugTextArrayPro[x, z] = new GameObject().AddComponent<Image>();
        image.transform.SetParent(canvas.transform, false);
        image.rectTransform.sizeDelta = new Vector2(cellSize - 0.001f, cellSize - 0.001f);
        image.transform.position = GetWorldPosition(x, z) + new Vector3(cellSize, 0.001f, cellSize) * 0.5f;
        image.sprite = sprite;

        var textMeshPro = new GameObject().AddComponent<TextMeshProUGUI>();
        textMeshPro.fontSize = 1;
        textMeshPro.alignment = TextAlignmentOptions.Center;
        textMeshPro.rectTransform.sizeDelta = new Vector2(cellSize, cellSize);
        textMeshPro.text = gridArray[x, z].ToString();
        textMeshPro.transform.SetParent(image.transform, false);
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize + origin;
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - origin).x / cellSize);
        z = Mathf.FloorToInt((worldPosition - origin).z / cellSize);
    }

    public void SetValue(int x, int z, int value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            gridArray[x, z] = Mathf.Clamp(value, HEAT_MAP_MIN_VALUE, HEAT_MAP_MAX_VALUE);
            debugTextArrayPro[x, z].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gridArray[x, z].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        SetValue(x, z, value);
    }

    public int GetValue(int x, int z)
    {
        if (IsWithinGrid(x, z))
        {
            return gridArray[x, z];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetValue(x, z);
    }

    public bool IsWithinGrid(int x, int z)
    {
        return (x >= 0 && z >= 0 && x < width && z < height);
    }

    public int GetHeight() => height;
    public int GetWidth() => width;
    public float GetCellSize() => cellSize;
}
