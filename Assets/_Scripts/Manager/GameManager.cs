using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PlayerTurn, EnemyTurn, Pause, Death}
public class GameManager : MonoBehaviour
{
    public static GameState CurrentState;
    private CharacterManager characterManager;


    public static Vector2 MousePos;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
