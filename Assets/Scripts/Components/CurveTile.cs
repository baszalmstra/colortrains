using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurveTile : Tile
{
    public TileDirection CurveDirection
    {
        get
        {
            switch (Direction)
            {
                case TileDirection.North:
                    return TileDirection.East;
                case TileDirection.East:
                    return TileDirection.South;
                case TileDirection.South:
                    return TileDirection.West;
                case TileDirection.West:
                    return TileDirection.North;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    
    // Move the train to the next tile
    public override void TrainEntered(Train train, TileDirection from)
    {
        if (from == Direction)
        {
            MoveTrainToNeighbor(train, from, CurveDirection);
        }
        else if (from == CurveDirection)
        {
            MoveTrainToNeighbor(train, from, Direction);
        }
        else
        {
            train.OnDeadEnd();
        }
    }
    
    private void OnDrawGizmos()
    {
        var scale = 1.5f;
        var from = new Vector3(0, 1, 0) + transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(from, Direction.DirectionVector3D()*scale);
        Gizmos.DrawRay(from, CurveDirection.DirectionVector3D()*scale);
    }
}
