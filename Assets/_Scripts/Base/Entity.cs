using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Entity : MonoBehaviour
{
    public Cell currCell;
    public StatsScriptable StatsScriptable;

    [SerializeField] private int maxHealth;
    public int MaxHealth { get { return maxHealth; } }
    public int currHealth;

    [SerializeField] private int maxMovePoints;
    public int MaxMovePoints { get { return maxMovePoints; } }
    public int currMovePoints;

    [SerializeField] private int attackDamage;
    public int AttackDamage { get {  return attackDamage; } }
    public int currAttackDamage;

    public delegate void MoveEventHandler();
    public event MoveEventHandler OnMovedCell;

    private void Awake()
    {
        maxHealth = StatsScriptable.HP;
        maxMovePoints = StatsScriptable.MOV;
        attackDamage = StatsScriptable.ATK;

        currHealth = MaxHealth;
        currMovePoints = MaxMovePoints;
    }

    public void MoveToCell(Cell cell)
    {
        if(currCell) currCell.Obj = null;
        currCell = null;

        currCell = cell;
        currCell.Obj = gameObject;
        transform.position = currCell.gameObject.transform.position;

        currMovePoints = maxMovePoints;

        OnMovedCell?.Invoke();
    }
}
