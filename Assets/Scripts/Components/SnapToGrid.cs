using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour
{
    public float grid = 3.0f;
    void Update()
    {
        var position = transform.localPosition;
        float x = Mathf.Round(position.x / grid) * grid;
        float y = Mathf.Round(position.z / grid) * grid;
        transform.localPosition = new Vector3(x,0.0f, y);
    }
}
