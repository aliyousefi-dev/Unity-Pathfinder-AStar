using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GridPathfindingDebugger : MonoBehaviour
{
    public GridPathfinding pathfinding;
    public GridCoordinateSystem2D grid;
    public Vector2Int startGridPosition = new Vector2Int(0, 0);
    public Vector2Int endGridPosition = new Vector2Int(10, 10);
    public Color pathColor = Color.cyan;

    private List<Vector2> currentPath;

    void Update()
    {
        if (pathfinding != null && grid != null)
        {
            currentPath = pathfinding.FindPathGrid(startGridPosition, endGridPosition);
        }
    }

    void OnDrawGizmos()
    {
        if (currentPath == null || currentPath.Count == 0) return;

        Gizmos.color = pathColor;
        for (int i = 0; i < currentPath.Count - 1; i++)
        {
            Gizmos.DrawLine(currentPath[i], currentPath[i + 1]);
            Gizmos.DrawSphere(currentPath[i], 0.1f);
        }
        Gizmos.DrawSphere(currentPath[^1], 0.1f);
    }
}
