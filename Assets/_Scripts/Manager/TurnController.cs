using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { Player, Enemy }

public class TurnController : MonoBehaviour
{
    [SerializeField] private TurnState CurrentTurnState;
    public TurnState CurrTurnState {
        get { return CurrentTurnState; }
        private set { 
            CurrentTurnState = value;
        }
    }

    public bool isInTurn = false;

    public Action StartTurn;
    public Action EndTurn;

    public event Action OnStartTurn;
    public event Action OnEndTurn;

    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private CharacterManager characterManager;

    private ITurn CurrentTurn;

    private void Start()
    {
        CurrTurnState = TurnState.Player;
        CurrentTurn = characterManager;

        StartTurn = CurrentTurn.StartTurn;
        EndTurn = CurrentTurn.EndTurn;

        OnStartTurn += StartTurn;
        OnEndTurn += EndTurn;

        StartCoroutine(InitiateTurn());
    }

    public void NextTurn()
    {
        if(!isInTurn)
        {
            ChangeTurn();
            StartCoroutine(InitiateTurn());
        }
    }

    public IEnumerator InitiateTurn()
    {
        isInTurn = true;
        OnStartTurn?.Invoke();
        
        yield return CurrentTurn != null ? StartCoroutine(CurrentTurn.Turn()) : null;

        OnEndTurn?.Invoke();
        isInTurn = false;
    }

    public void ChangeTurn()
    {
        OnStartTurn -= StartTurn;
        OnEndTurn -= EndTurn;

        if (CurrentTurnState == TurnState.Enemy)
        {
            CurrentTurnState = TurnState.Player;
            CurrentTurn = characterManager;
        }
        else if (CurrentTurnState == TurnState.Player)
        {
            CurrTurnState = TurnState.Enemy;
            CurrentTurn = enemyManager;
        }

        StartTurn = CurrentTurn.StartTurn;
        EndTurn = CurrentTurn.EndTurn;

        OnStartTurn += StartTurn;
        OnEndTurn += EndTurn;

        Debug.Log("Turn Changed");
    }
}
