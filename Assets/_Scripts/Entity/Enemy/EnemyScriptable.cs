using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EnemyScriptable : ScriptableObject
{
    public string enemyName;
    public Sprite sprite;
    public int cost;

    [Space(15)]

    //public int detectionRange = 3;
    public int maxRangeFromTarget = 0;
    //public int rangeFromCaster = 2;
    public bool rangeFromCaster = false;

    [Space(15)]
    public int attackRange = 0;
    public int attackWidth = 2;
    public HighlightShape attackShape;

    [Space(15)]
    public bool isPerpendicularToTarget;

    public void OnValidate()
    {
        maxRangeFromTarget = Mathf.Max(maxRangeFromTarget, 1);
        //rangeFromCaster = Mathf.Clamp(rangeFromCaster, 0, maxRangeFromTarget);
        attackRange = Math.Max(attackRange, maxRangeFromTarget);
    }

    public virtual Character PrepareAttack(Enemy caster, out List<Cell> attackArea, out List<Cell> rangeArea)
    {
        attackArea = new List<Cell>();
        rangeArea = new List<Cell>();

        rangeArea = CellsHighlighter.HighlightArea(caster.occupiedCell.index, maxRangeFromTarget, HighlightShape.Diamond);

        var characters = rangeArea
            .Where((cell) => cell.isOccupied)
            .Select((cell) => cell.occupiedEntity.GetComponent<Character>())
            .Where((cell) => cell != null)
            .ToList();

        var mainTarget = characters.Any() ? characters.First() : null;

        if (mainTarget)
        {
            var dir = CellsHighlighter.GetDirection(caster.transform.position, mainTarget.transform.position);
            attackArea = CellsHighlighter.HighlightArea(
                rangeFromCaster ? caster.occupiedCell.index : mainTarget.occupiedCell.index, 
                attackWidth, attackShape, attackRange, dir
                );
            if(rangeFromCaster) attackArea.Remove(caster.occupiedCell);
        }

        Debug.Log(mainTarget);

        return mainTarget;
    }

    public abstract bool Attack(Enemy from, List<Cell> attackArea);
}
