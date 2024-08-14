using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TurnState { Player, Enemy }

public class TurnController : MonoBehaviour
{
    public TurnState currTurnState;

    //public bool isInTurn = false;

    public event Action OnStartTurn;
    public event Action OnEndTurn;

    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private CharacterManager characterManager;

    private ITurn CurrentTurn;

    private void Start()
    {
        currTurnState = TurnState.Player;
        CurrentTurn = characterManager;
        CurrentTurn.StartTurn();

        //OnStartTurn += CurrentTurn.StartTurn;
        //OnEndTurn += CurrentTurn.EndTurn;
    }

    public void EndTurn()
    {
        if (CurrentTurn != null)
        {
            OnStartTurn?.Invoke();

            enemyManager.StartTurn();
            characterManager.StartTurn();

            OnEndTurn?.Invoke();
        }
    }

    public void ChangeTurn()
    {
        //OnStartTurn -= CurrentTurn.StartTurn;
        //OnEndTurn -= CurrentTurn.EndTurn;

        if (currTurnState == TurnState.Enemy)
        {
            currTurnState = TurnState.Player;
            CurrentTurn = characterManager;
        }
        else if (currTurnState == TurnState.Player)
        {
            currTurnState = TurnState.Enemy;
            CurrentTurn = enemyManager;
        }

        //OnStartTurn += CurrentTurn.StartTurn;
        //OnEndTurn += CurrentTurn.EndTurn;
    }
}
