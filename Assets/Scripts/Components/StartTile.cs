using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor;
using UnityEngine;

public class StartTile : Tile
{
    public GameObject train;

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.OnSimulationStarted += OnSimulationStarted;
    }

    // Move the train to the next tile
    public override void TrainEntered(Train train, TileDirection from)
    {
        train.OnDeadEnd();
    }
    
    /// <summary>
    /// Spawn a train at the start of the simulation
    /// </summary>
    private void OnSimulationStarted()
    {
        var trainInstance = PrefabUtility.InstantiatePrefab(this.train) as GameObject;
        var train = trainInstance.GetComponent<Train>();
        train.transform.position = transform.position + new Vector3(0, 1.2f, 0);
        train.transform.forward = transform.forward;

        MoveTrainToNeighbor(train, Direction.Opposite(), Direction);
    }
    
    private void OnDrawGizmos()
    {
        var scale = 1.5f;
        var from = new Vector3(0, 1, 0) + transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(from, Direction.DirectionVector3D()*scale);
    }
}
