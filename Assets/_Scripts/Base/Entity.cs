using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private Cell currentCell;

    [SerializeField] private int maxHealth = 100;
    public int MaxHealth { get { return maxHealth; } }
    public int currHealth;

    [SerializeField] private int maxMovePoints = 20;
    public int MaxMovePoints { get { return maxMovePoints; } }
    public int currMovePoints;

    [SerializeField] private int moveCost;
    [SerializeField] private int moveRange;

    public abstract bool ValidateMove(Cell cell);

    public void TakeDmg(int dmg)
    {
        currHealth = Mathf.Clamp(currHealth - dmg, 0, maxHealth);
    }

    public void MoveToCell(Cell cell)
    {
        if (!ValidateMove(cell)) return;
        currentCell.Obj = null;
        currentCell = cell;
        cell.Obj = gameObject;
    }
}
