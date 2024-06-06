using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Entity : MonoBehaviour
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

    private void Awake()
    {
        maxHealth = StatsScriptable.HP;
        maxMovePoints = StatsScriptable.MOV;
        attackDamage = StatsScriptable.ATK;

        currHealth = MaxHealth;
        currMovePoints = MaxMovePoints;
    }

    public abstract bool ValidateMove(Cell cell);

    public void TakeDmg(int dmg)
    {
        currHealth = Mathf.Clamp(currHealth - dmg, 0, maxHealth);
    }

    public virtual void MoveToCell(Cell cell)
    {
        if (ValidateMove(cell) == false) return;
        if(currCell) currCell.Obj = null;
        currCell = null;

        currCell = cell;
        currCell.Obj = gameObject;
        transform.position = currCell.gameObject.transform.position;
    }
}
