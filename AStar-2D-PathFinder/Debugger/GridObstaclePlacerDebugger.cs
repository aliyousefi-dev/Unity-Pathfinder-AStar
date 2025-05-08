using UnityEngine;

[ExecuteAlways]
public class GridObstaclePlacerDebugger : MonoBehaviour
{
    public GridCoordinateSystem2D grid;
    public Vector2Int center;
    public int size = 1;

    private Vector2Int previousCenter;
    private int previousSize;

    private ObstacleManager2D obstacleManager;

    private void OnEnable()
    {
        if (grid != null)
        {
            obstacleManager = grid.GetComponent<ObstacleManager2D>();
        }
    }

    private void Update()
    {
        if (grid == null || obstacleManager == null) return;

        // Remove previous obstacles
        UpdateObstacleArea(previousCenter, previousSize, false);

        // Add new obstacles
        UpdateObstacleArea(center, size, true);

        // Save current state
        previousCenter = center;
        previousSize = size;
    }

    private void UpdateObstacleArea(Vector2Int areaCenter, int areaSize, bool isObstacle)
    {
        if (obstacleManager == null) return;

        int half = areaSize / 2;
        for (int x = -half; x <= half; x++)
        {
            for (int y = -half; y <= half; y++)
            {
                Vector2Int pos = areaCenter + new Vector2Int(x, y);
                obstacleManager.SetObstacle(pos, isObstacle);
            }
        }
    }

    private void OnDisable()
    {
        // Clear the area if this object is disabled or deleted
        if (grid != null && obstacleManager != null)
        {
            UpdateObstacleArea(center, size, false);
        }
    }
}
