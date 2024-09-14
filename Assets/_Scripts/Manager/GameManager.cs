using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameState { Pause, Play, Death, Win}
public class GameManager : MonoBehaviour
{
    public GameState currentState { get; private set; }

    public static Vector2 MousePos;
    public static Camera mainCam;

    public bool isPaused { get; private set; }

    public UnityEvent OnPause;
    public UnityEvent OnResume;
    public UnityEvent OnDeath;
    public UnityEvent OnWin;

    public static GameManager Instance;

    public static int currentRound = 1;

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
        //Debug.Log(currentRound);
        mainCam = Camera.main;
    }

    private void Update()
    {
        MousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        if (!EnemyManager.Instance.ActiveEnemies.Any())
        {
            Win();
        } else if (!CharacterManager.instance.ActiveCharacters.Any())
        {
            Dead();
            currentRound = 1;
        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        currentState = GameState.Pause;
        OnPause?.Invoke();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Play;
        OnResume?.Invoke();
    }

    public void Dead()
    {
        currentState = GameState.Death;
        OnDeath?.Invoke();
    }

    public void Win()
    {
        currentState = GameState.Win;
        OnWin?.Invoke();
    }

    public void NextRound()
    {
        SceneLoader.Instance.ReloadScene();
        currentRound++;
    }
}
