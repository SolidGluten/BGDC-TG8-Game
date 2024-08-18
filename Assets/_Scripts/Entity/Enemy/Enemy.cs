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

    private List<Cell> detectionArea = new List<Cell>(); 
    private List<Cell> attackArea = new List<Cell>();
    private List<Cell> rangeArea = new List<Cell>();

    public void PrepareAttack()
    {
        if (!isTargetInRange) return;

        CellsHighlighter.SetTypes(rangeArea, CellType.Enemy_TargetRange, false);
        rangeArea = CellsHighlighter.HighlightArea(occupiedCell.index, enemyScriptable.attackWidth, HighlightShape.Square);
        CellsHighlighter.SetTypes(rangeArea, CellType.Enemy_TargetRange, true);

        var characters = rangeArea
            .Where((cell) => cell.isOccupied)
            .Select((cell) => cell.occupiedEntity.GetComponent<Character>())
            .ToList();

        var mainTarget = characters.Any() ? characters.First() : null;

        if (mainTarget)
        {
            CellsHighlighter.SetTypes(attackArea, CellType.Enemy_Attack, false);
            attackArea = CellsHighlighter.HighlightArea(mainTarget.occupiedCell.index, enemyScriptable.attackWidth, enemyScriptable.attackShape);
            CellsHighlighter.SetTypes(attackArea, CellType.Enemy_Attack, true);

            target = attackArea
                .Where((cell) => cell.isOccupied)
                .Select((cell) => cell.occupiedEntity.GetComponent<Character>())
                .ToArray();

            isAttackReady = true;
        }
    }

    public void Attack()
    {
        enemyScriptable.Attack(this, target);
        CellsHighlighter.SetTypes(attackArea, CellType.Enemy_Attack, false);
        isAttackReady = false;
    }

    public void HighlightDetectionArea()
    {
        detectionArea = CellsHighlighter.HighlightArea(occupiedCell.index, enemyScriptable.detectionRange, HighlightShape.Circle);

        foreach (var cell in detectionArea)
            cell.Types = EnumFlags.RaiseFlag(cell.Types, CellType.Enemy_Detection);
    }

    public void HighlightRangeArea()
    {
        rangeArea = CellsHighlighter.HighlightArea(occupiedCell.index, enemyScriptable.maxRangeFromTarget, HighlightShape.Diamond);

        foreach (var cell in rangeArea)
            cell.Types = EnumFlags.RaiseFlag(cell.Types, CellType.Enemy_MaxRange);

        rangeArea = detectionArea;
    }
}
