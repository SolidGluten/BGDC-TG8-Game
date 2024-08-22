using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EnemyState { Idle, InRange, ReadyAttack }

public class Enemy : Entity
{
    public EnemyScriptable enemyScriptable;

    public bool isTargetInRange = false;
    public bool isAttackReady = false;

    public Character target;

    private SpriteRenderer _renderer; 

    private List<Cell> _detectionArea = new List<Cell>();
    private List<Cell> _maxRangeArea = new List<Cell>();

    private List<Cell> _attackArea = new List<Cell>();
    private List<Cell> _rangeArea = new List<Cell>();

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = enemyScriptable.sprite;
    }

    public void PrepareAttack()
    {
        if (!isTargetInRange) {
            isAttackReady = false;
            return;
        }

        if (!isAttackReady)
        {
            target = enemyScriptable.PrepareAttack(this, out _attackArea, out _rangeArea);
            if (!target) return;
            CellsHighlighter.RaiseLayerType(_attackArea, CellType.Enemy_Attack);
            CellsHighlighter.RaiseLayerType(_rangeArea, CellType.Enemy_TargetRange);
            isAttackReady = true;
        } else { 
            enemyScriptable.Attack(this, _attackArea);
            isAttackReady = false;
        }
    }

    public void HighlightDetectionArea()
    {
        _detectionArea = CellsHighlighter.HighlightArea(occupiedCell.index, enemyScriptable.detectionRange, HighlightShape.Circle);
        CellsHighlighter.RaiseLayerType(_detectionArea, CellType.Enemy_Detection);
    }

    public void HighlightMaxRangeArea()
    {
        _maxRangeArea = CellsHighlighter.HighlightArea(occupiedCell.index, enemyScriptable.maxRangeFromTarget, HighlightShape.Diamond);
        CellsHighlighter.RaiseLayerType(_maxRangeArea, CellType.Enemy_MaxRange);
    }
}
