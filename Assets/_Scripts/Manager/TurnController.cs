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
    public event Action OnStartTurn;
    public event Action OnEndTurn;

    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private CharacterManager characterManager;

    private ITurn CurrentTurn;

    private void Awake()
    {
        OnEndTurn += ChangeTurn;
    }

    private void Start()
    {
        CurrTurnState = TurnState.Player;
        CurrentTurn = characterManager;
        StartCoroutine(InitiateTurn());
    }

    public void InitiateTurnWrap()
    {
        if(!isInTurn) StartCoroutine(InitiateTurn());
    }

    public IEnumerator InitiateTurn()
    {
        OnStartTurn?.Invoke();
        isInTurn = true;
        
        yield return CurrentTurn != null ? StartCoroutine(CurrentTurn.Turn()) : null;

        OnEndTurn?.Invoke();
        isInTurn = false;
    }

    public void ChangeTurn()
    {
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
    }
}
