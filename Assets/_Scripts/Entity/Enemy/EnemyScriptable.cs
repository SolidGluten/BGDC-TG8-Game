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

    public int detectionRange = 3;
    public int maxRangeFromTarget = 0;
    public int rangeFromCaster = 2;
    public int attackRange = 0;
    public int attackWidth = 2;
    public HighlightShape attackShape;

    public virtual Character[] PrepareAttack(Enemy caster, out List<Cell> attackArea, out List<Cell> rangeArea)
    {
        attackArea = new List<Cell>();
        rangeArea = new List<Cell>();

        Character[] target = Array.Empty<Character>();

        rangeArea = CellsHighlighter.HighlightArea(caster.occupiedCell.index, rangeFromCaster, HighlightShape.Diamond);

        var characters = rangeArea
            .Where((cell) => cell.isOccupied)
            .Select((cell) => cell.occupiedEntity.GetComponent<Character>())
            .Where((cell) => cell != null)
            .ToList();

        var mainTarget = characters.Any() ? characters.First() : null;

        if (mainTarget)
        {
            var dir = CellsHighlighter.GetDirection(caster.transform.position, mainTarget.transform.position);
            attackArea = CellsHighlighter.HighlightArea(mainTarget.occupiedCell.index, attackWidth, attackShape, attackRange, dir);
            target = attackArea
                .Where((cell) => cell.isOccupied)
                .Select((cell) => cell.occupiedEntity.GetComponent<Character>())
                .Where((cell) => cell != null)
                .ToArray();
        }

        return target;
    }

    public abstract void Attack(Enemy from, Character[] targets, List<Cell> attackArea = null);
}
