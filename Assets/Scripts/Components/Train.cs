using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    private GridManager _grid;

    public float speed = 3.0f;
    public float groundOffset = 1.2f;

    public Vector3 GroundOffsetVector
    {
        get
        {
            return new Vector3(0.0f, groundOffset, 0.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnSimulationEnded += OnSimulationEnded;
    }


    /// <summary>
    /// Called when the simulation ends
    /// </summary>
    private void OnSimulationEnded()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnSimulationEnded -= OnSimulationEnded;
    }
    
    public delegate void ArrivedDelegate();
    public IEnumerator MoveLinearTo(Vector3 position, ArrivedDelegate arrived)
    {
        float elapsedTime = 0;
        var startingPos = transform.position;
        float time = (position - transform.position).magnitude / speed;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, position, (elapsedTime / time));
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        arrived();
    }

    /// <summary>
    /// Called when the train reaches a dead end
    /// </summary>
    public void OnDeadEnd()
    {
        Destroy(gameObject);
    }
}
