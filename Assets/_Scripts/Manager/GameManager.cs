using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState { Pause, Play, Death}
public class GameManager : MonoBehaviour
{
    public static GameState CurrentState { get; private set; }

    public static Vector2 MousePos;
    public static Camera mainCam;

    [SerializeField] private TurnController turnController;

    public bool isPaused { get; private set; }

    public event Action OnPause;
    public event Action OnResume;

    public static GameManager Instance;

    //Singleton
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        MousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        OnPause();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        OnResume();
    }

}
