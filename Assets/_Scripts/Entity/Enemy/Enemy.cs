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

    public List<Cell> attackArea = new List<Cell>();
    private List<Cell> detectionArea = new List<Cell>();
    private List<Cell> rangeArea = new List<Cell>();

    public void ReadyAttack()
    {
        if (!isTargetInRange) return;

        if (attackArea.Any())
        {
            foreach (var cell in attackArea)
                cell.Types = EnumFlags.LowerFlag(cell.Types, CellType.Enemy_Range);
        }

        attackArea = CellsHighlighter.HighlightArea(occupiedCell.index, enemyScriptable.attackRange, enemyScriptable.attackShape);

        foreach (var cell in attackArea)
            cell.Types = EnumFlags.RaiseFlag(cell.Types, CellType.Enemy_Range);
    }

    public void Attack()
    {

    }

    public void HighlightDetectionArea()
    {
        //if (detectionArea.Any())
        //{
        //    foreach(var cell in detectionArea)
        //        cell.Types = EnumFlags.LowerFlag(cell.Types, CellType.Enemy_Detection);
        //}

        detectionArea = CellsHighlighter.HighlightArea(occupiedCell.index, enemyScriptable.detectionRange, HighlightShape.Square);

        foreach (var cell in detectionArea)
            cell.Types = EnumFlags.RaiseFlag(cell.Types, CellType.Enemy_Detection);
    }

    public void HighlightRangeArea()
    {
        //if (rangeArea.Any())
        //{
        //    foreach (var cell in rangeArea)
        //        cell.Types = EnumFlags.LowerFlag(cell.Types, CellType.Enemy_Range);
        //}

        rangeArea = CellsHighlighter.HighlightArea(occupiedCell.index, enemyScriptable.maxRangeFromTarget, HighlightShape.Square);

        foreach (var cell in rangeArea)
            cell.Types = EnumFlags.RaiseFlag(cell.Types, CellType.Enemy_Range);

        rangeArea = detectionArea;
    }
}
