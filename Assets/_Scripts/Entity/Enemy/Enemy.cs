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

    public Character[] target;

    private List<Cell> _detectionArea = new List<Cell>();
    private List<Cell> _maxRangeArea = new List<Cell>();

    private List<Cell> _attackArea = new List<Cell>();
    private List<Cell> _rangeArea = new List<Cell>();

    public void PrepareAttack()
    {
        target = enemyScriptable.PrepareAttack(this, out List<Cell> attackArea, out List<Cell> rangeArea);
        if (!target.Any()) {
            isAttackReady = false;
            return;
        }

        isAttackReady = true;

        _attackArea = new List<Cell>(attackArea);
        _rangeArea = new List<Cell>(rangeArea);

        CellsHighlighter.RaiseLayerType(_attackArea, CellType.Enemy_Attack);
        CellsHighlighter.RaiseLayerType(_rangeArea, CellType.Enemy_TargetRange);
    }

    public void Attack()
    {
        enemyScriptable.Attack(this, target, _attackArea);
        isAttackReady = false;
    }

    public void HighlightDetectionArea()
    {
        _detectionArea = CellsHighlighter.HighlightArea(occupiedCell.index, enemyScriptable.detectionRange, HighlightShape.Circle);

        foreach (var cell in _detectionArea)
            cell.Types = EnumFlags.RaiseFlag(cell.Types, CellType.Enemy_Detection);
    }

    public void HighlightMaxRangeArea()
    {
        _maxRangeArea = CellsHighlighter.HighlightArea(occupiedCell.index, enemyScriptable.maxRangeFromTarget, HighlightShape.Diamond);

        foreach (var cell in _maxRangeArea)
            cell.Types = EnumFlags.RaiseFlag(cell.Types, CellType.Enemy_MaxRange);

        _maxRangeArea = _detectionArea;
    }
}
