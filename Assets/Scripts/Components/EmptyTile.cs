using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : Tile
{
    public override void TrainEntered(Train train, TileDirection from)
    {
        train.OnDeadEnd();
    }
}
