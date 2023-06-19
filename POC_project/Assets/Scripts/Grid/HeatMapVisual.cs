using UnityEngine;

public class HeatMapVisual : MonoBehaviour
{
    private GenericGrid grid;
    private Mesh mesh;
    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(GenericGrid grid)
    {
        this.grid = grid;
        UpdateHeatMapVisual();
    }

    private void UpdateHeatMapVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uvs, out int[] triangles);

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int z = 0; z < grid.GetHeight(); z++)
            {
                int index = x * grid.GetHeight() + z;
                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();
                Debug.Log(grid.GetWorldPosition(x, z) + quadSize * 0.5f);
                MeshUtils.AddToMeshArrays(vertices, uvs, triangles, index, grid.GetWorldPosition(x, z) + quadSize * 0.5f, 0, quadSize, Vector3.zero, Vector3.zero);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
    }
}
