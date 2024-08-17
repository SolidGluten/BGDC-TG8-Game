using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EnemyState { Idle, Move, AttackReady }

public class Enemy : Entity
{
    public EnemyState state = EnemyState.Idle;
    public EnemyScriptable enemyScriptable;
    public List<Cell> attackArea = new List<Cell>();

    public void ReadyAttack()
    {
        if (attackArea.Any())
        {
            foreach (var cell in attackArea)
                cell.Types = EnumFlags.LowerFlag(cell.Types, CellType.Enemy_Range);
        }

        attackArea = CellsHighlighter.HighlightArea(occupiedCell.index, enemyScriptable.attackRange, enemyScriptable.attackShape);

        foreach (var cell in attackArea)
            cell.Types = EnumFlags.RaiseFlag(cell.Types, CellType.Enemy_Range);
    }
}
