using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ObstacleManager2D : MonoBehaviour
{
    private HashSet<Vector2Int> obstacles = new HashSet<Vector2Int>();

    public void SetObstacle(Vector2Int position, bool isObstacle)
    {
        if (isObstacle)
        {
            obstacles.Add(position);
        }
        else
        {
            obstacles.Remove(position);
        }
    }

    public bool IsObstacle(Vector2Int position)
    {
        return obstacles.Contains(position);
    }

    public IEnumerable<Vector2Int> GetAllObstacles()
    {
        return obstacles;
    }

    public Vector2Int? FindNearestNonObstacle(Vector2Int targetGrid, int searchRadius, System.Func<Vector2Int, bool> isInBounds)
    {
        for (int radius = 1; radius <= searchRadius; radius++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    Vector2Int candidate = targetGrid + new Vector2Int(x, y);
                    if (isInBounds(candidate) && !IsObstacle(candidate))
                    {
                        return candidate;
                    }
                }
            }
        }

        return null;
    }
}