using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class GridRenderDebugger : MonoBehaviour
{
    public Color gridColor = Color.green;
    public Color fontColor = Color.white;
    public Color obstacleColor = Color.red;
    public bool showLabels = true;
    public int fontSize = 10;

    public GridCoordinateSystem2D grid;
    private ObstacleManager2D obstacleManager;

    private void OnEnable()
    {
        if (grid != null)
        {
            obstacleManager = grid.GetComponent<ObstacleManager2D>();
        }
    }

    private void OnDrawGizmos()
    {
        if (grid == null || obstacleManager == null) return;

        float cellSize = grid.CellSize;
        Vector2 origin = grid.Origin;
        int subdivisions = grid.subdivisions;

        // Draw the grid
        Gizmos.color = gridColor;
        for (int y = 0; y <= subdivisions; y++)
            Gizmos.DrawLine(origin + new Vector2(0, y * cellSize), origin + new Vector2(grid.scale, y * cellSize));
        for (int x = 0; x <= subdivisions; x++)
            Gizmos.DrawLine(origin + new Vector2(x * cellSize, 0), origin + new Vector2(x * cellSize, grid.scale));

        // Draw obstacles
        Gizmos.color = obstacleColor;
        foreach (Vector2Int obstacle in obstacleManager.GetAllObstacles())
        {
            Vector3 worldPosition = grid.GridToWorld(obstacle);
            Gizmos.DrawCube(worldPosition, Vector3.one * cellSize * 0.9f);
        }

        // Draw labels
#if UNITY_EDITOR
        if (showLabels)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = fontColor;
            style.fontSize = fontSize;

            for (int x = 0; x < subdivisions; x++)
            {
                for (int y = 0; y < subdivisions; y++)
                {
                    Vector2 worldPos = grid.GridToWorld(new Vector2Int(x, y));
                    Vector3 screenPoint = Handles.matrix.MultiplyPoint(worldPos);
                    if (Camera.current != null)
                    {
                        screenPoint = Camera.current.WorldToScreenPoint(worldPos);
                        if (screenPoint.z > 0) // Only draw if point is in front of camera
                        {
                            Handles.Label(worldPos, $"({x},{y})", style);
                        }
                    }
                }
            }
        }
#endif
    }
}
