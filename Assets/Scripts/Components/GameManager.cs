using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    
    public event Action OnSimulationStarted;
    public event Action OnSimulationEnded;

    public bool SimulationRunning { get; private set; }

    public SceneReference [] levels;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // Sets this to not be destroyed when reloading scenes
        DontDestroyOnLoad(gameObject);
        
        // Init default values
        SimulationRunning = false;
        
        // Load the first level
        SceneManager.LoadScene(levels[0], LoadSceneMode.Additive);
    }
    
    private void OnGUI()
    {
        if (SimulationRunning)
        {
            if (GUI.Button(new Rect(10, 10, 150, 100), "End simulation"))
            {
                EndSimulation();
            }
        }
        else
        {
            if (GUI.Button(new Rect(10, 10, 150, 100), "Start simulation"))
            {
                StartSimulation();
            }
        }
    }

    private void StartSimulation()
    {
        SimulationRunning = true;
        OnSimulationStarted?.Invoke();
    }

    private void EndSimulation()
    {
        OnSimulationEnded?.Invoke();
        SimulationRunning = false;
    }
}
