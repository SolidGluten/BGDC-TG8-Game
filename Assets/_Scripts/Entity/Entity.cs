using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class Entity : MonoBehaviour
{
    public Cell CurrCell;
    public StatsScriptable Stats;

    public int currHealth;
    public int currMovePoints;
    public int currAttackDamage;

    public event Action OnFinishMove;

    private void Awake()
    {
        currHealth = Stats.HP;
        currMovePoints = Stats.MOV;
    }

    public bool Move(Cell cell)
    {
        if(cell.isOccupied) return false;

        if(CurrCell) CurrCell.Obj = null;
        CurrCell = null;

        CurrCell = cell;
        CurrCell.Obj = gameObject;
        transform.position = CurrCell.gameObject.transform.position;

        currMovePoints = Stats.MOV;

        OnFinishMove?.Invoke();

        return true;
    }
}
