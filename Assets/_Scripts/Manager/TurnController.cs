using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    //public event Action OnStartTurn;
    //public event Action OnEndTurn;

    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private CharacterManager characterManager;

    private ITurn CurrentTurn;

    private void Start()
    {
        CurrTurnState = TurnState.Player;
        CurrentTurn = characterManager;
        CurrentTurn.StartTurn();

        //OnStartTurn += CurrentTurn.StartTurn;
        //OnEndTurn += CurrentTurn.EndTurn;
    }

    public void EndTurn()
    {
        if (CurrentTurn != null)
        {
            CurrentTurn.EndTurn();
            ChangeTurn();
            CurrentTurn.StartTurn();
        }
    }

    public void ChangeTurn()
    {
        //OnStartTurn -= CurrentTurn.StartTurn;
        //OnEndTurn -= CurrentTurn.EndTurn;

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

        //OnStartTurn += CurrentTurn.StartTurn;
        //OnEndTurn += CurrentTurn.EndTurn;
    }
}
