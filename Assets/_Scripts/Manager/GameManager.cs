using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PlayerTurn, EnemyTurn, Pause, Death}
public class GameManager : MonoBehaviour
{
    public static GameState currentState;
    public GridSystem grid;

    private void Start()
    {
        grid.AddObj(new GameObject(), new Vector2Int(0, 1));
    }

}
