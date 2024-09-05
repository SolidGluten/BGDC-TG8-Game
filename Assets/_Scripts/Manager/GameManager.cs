using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Pause, Play, Death}
public class GameManager : MonoBehaviour
{
    public static GameState CurrentState { get; private set; }

    public static Vector2 MousePos;
    public static Camera mainCam;

    public bool isPaused { get; private set; }

    public event Action OnPause;
    public event Action OnResume;

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
            // Player win
            //SceneLoader.Instance.ReloadScene();
            currentRound++;
        } else if (!CharacterManager.instance.ActiveCharacters.Any())
        {
            // Player lost
            //SceneLoader.Instance.ReloadScene();
            currentRound = 1;
        }

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
