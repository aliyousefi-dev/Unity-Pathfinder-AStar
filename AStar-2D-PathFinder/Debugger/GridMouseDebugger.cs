using UnityEngine;

public class GridMouseDebugger : MonoBehaviour
{
    public GridCoordinateSystem2D gridCoordinateSystem2D;
    private ObstacleManager2D obstacleManager;

    private void OnEnable()
    {
        if (gridCoordinateSystem2D != null)
        {
            obstacleManager = gridCoordinateSystem2D.GetComponent<ObstacleManager2D>();
        }

        InputManager.OnLeftMousePressed += OnLeftMousePressed;
        InputManager.OnRightMousePressed += OnRightMousePressed;
    }

    private void OnDisable()
    {
        InputManager.OnLeftMousePressed -= OnLeftMousePressed;
        InputManager.OnRightMousePressed -= OnRightMousePressed;
    }

    private void OnLeftMousePressed(Vector2 screenPos)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane));

        if (gridCoordinateSystem2D.ContainsWorldPosition(worldPos))
        {
            Vector2Int gridPos = gridCoordinateSystem2D.WorldToGrid(worldPos);
            Debug.Log($"Grid Position: {gridPos}");
        }
        else
        {
            Debug.Log("Clicked outside of the grid.");
        }
    }

    private void OnRightMousePressed(Vector2 screenPos)
    {
        if (obstacleManager == null || gridCoordinateSystem2D == null) return;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane));

        if (gridCoordinateSystem2D.ContainsWorldPosition(worldPos))
        {
            Vector2Int gridPos = gridCoordinateSystem2D.WorldToGrid(worldPos);
            obstacleManager.SetObstacle(gridPos, true);
            Debug.Log($"Obstacle placed at: {gridPos}");
        }
        else
        {
            Debug.Log("Clicked outside of the grid.");
        }
    }
}
