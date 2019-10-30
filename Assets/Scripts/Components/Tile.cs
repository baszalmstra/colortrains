using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public enum TileDirection
{
    North,
    East,
    South,
    West
}

public static class TileDirectionExt
{
    public static TileDirection Opposite(this TileDirection direction)
    {
        switch (direction)
        {
            case TileDirection.North:
                return TileDirection.South;
            case TileDirection.East:
                return TileDirection.West;
            case TileDirection.South:
                return TileDirection.North;
            case TileDirection.West:
                return TileDirection.East;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
    
    public static Vector2Int DirectionVector2D(this TileDirection direction)
    {
        switch (direction)
        {
            case TileDirection.North:
                return Vector2Int.up;
            case TileDirection.East:
                return Vector2Int.right;
            case TileDirection.South:
                return Vector2Int.down;
            case TileDirection.West:
                return Vector2Int.left;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
    
    public static Vector3 DirectionVector3D(this TileDirection direction)
    {
        var vec = DirectionVector2D(direction);
        return new Vector3(vec.x, 0.0f, vec.y);
    }
}

[RequireComponent(typeof(SnapToGrid))]
public abstract class Tile : MonoBehaviour
{
    private GridManager _gridManager;
    protected SnapToGrid Snap;

    public Vector2Int GridPosition
    {
        get
        {
            var pos = transform.localPosition;
            return new Vector2Int((int) (pos.x / 3.0f),  (int) (pos.z / 3.0f));
        }
    }
    
    /// <summary>
    /// Returns the wind direction to where this tile is facing.
    /// </summary>
    public TileDirection Direction
    {
        get
        {
            var pos = GridFacing;
            if (pos.y > 0.9)
                return TileDirection.North;
            else if (pos.y < -0.9)
                return TileDirection.South;
            else if (pos.x > 0.9)
                return TileDirection.East;
            else if (pos.x < -0.9)
                return TileDirection.West;
            else
                throw new ArgumentException();
        }
    }
    
    /// <summary>
    /// Returns a 2D vector that describes the facing of the tile.
    /// </summary>
    public Vector2Int GridFacing
    {
        get
        {
            var pos = transform.right;
            return new Vector2Int((int) pos.x, (int) pos.z);
        }
    }

    /// <summary>
    /// Returns the neighbor of this tile in the specified direction or null if not such tile exists.
    /// </summary>
    public Tile GetNeighbor(TileDirection direction)
    {
        return _gridManager.GetTileAt(GridPosition + direction.DirectionVector2D());
    }
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Snap = GetComponent<SnapToGrid>();
        
        _gridManager = this.transform.parent.gameObject.GetComponent<GridManager>();
        _gridManager.RegisterTile(this);
    }

    // Called when a train enters this tile.
    public abstract void TrainEntered(Train train, TileDirection from);
    
    /// <summary>
    /// Start a coroutine to move the train to the specified direction given that the train is coming from a specific
    /// direction.
    /// </summary>
    protected void MoveTrainToNeighbor(Train train, TileDirection from, TileDirection to)
    {
        train.StartCoroutine(
            train.MoveLinearTo(
                transform.position + train.GroundOffsetVector + 0.5f * Snap.grid * to.DirectionVector3D(),
                () =>
                {
                    Tile neighbor = GetNeighbor(to);
                    if (neighbor == null)
                        train.OnDeadEnd();
                    else
                        neighbor.TrainEntered(train, to.Opposite());
                }));
    }
}
