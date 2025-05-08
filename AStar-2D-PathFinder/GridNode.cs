using UnityEngine;

public class GridNode
{
    public Vector2Int GridPosition;
    public Vector2 WorldPosition;
    public float GCost = float.MaxValue;
    public float HCost;
    public float FCost => GCost + HCost;
    public GridNode Parent;

    public GridNode(Vector2Int gridPos, Vector2 worldPos)
    {
        GridPosition = gridPos;
        WorldPosition = worldPos;
    }
}
