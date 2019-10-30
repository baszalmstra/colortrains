using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Dictionary<Vector2Int, Tile> tiles_ = new Dictionary<Vector2Int, Tile>();
    
    /// <summary>
    /// Called to register a tile with this instance.
    /// </summary>
    public void RegisterTile(Tile tile)
    {
        tiles_.Add(tile.GridPosition, tile);
    }

    
    /// <summary>
    /// Returns the tile at the given position or null if no such tile exists.
    /// </summary>
    public Tile GetTileAt(Vector2Int gridPosition)
    {
        Tile tile;
        tiles_.TryGetValue(gridPosition, out tile);
        return tile;
    }
}
