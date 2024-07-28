using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Cell occupiedCell;
    public StatsScriptable stats;

    public int currHealth;
    public int currMovePoints;
    public int currAttackDamage;

    public void Start()
    {
        currHealth = stats.HP;
        currMovePoints = stats.MOV;
        currAttackDamage = stats.ATK;
    }

    public void DestroySelf()
    {
        occupiedCell.occupiedEntity = null;
        Destroy(gameObject);
    }
}
