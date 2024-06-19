using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState { PlayerTurn, EnemyTurn, Pause, Death}
public class GameManager : MonoBehaviour
{
    public static GameState CurrentState { get; private set; }

    public CharacterManager characterManager;
    public EnemyManager enemyManager;

    public static Vector2 MousePos;
    private Camera cam;

    public bool isPaused { get; private set; }

    public static GameManager Instance;

    public event Action OnPause;
    public event Action OnResume;

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
        cam = Camera.main;
    }

    private void Update()
    {
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
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
