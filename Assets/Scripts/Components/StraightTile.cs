using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightTile : Tile
{
    // Move the train to the next tile
    public override void TrainEntered(Train train, TileDirection from)
    {
        if (from == Direction)
        {
            MoveTrainToNeighbor(train, from, Direction.Opposite());
        }
        else if (from == Direction.Opposite())
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
        var from = new Vector3(0, 1, 0) + transform.position - Direction.DirectionVector3D()*scale;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(from, Direction.DirectionVector3D()*scale*2);
    }
}
