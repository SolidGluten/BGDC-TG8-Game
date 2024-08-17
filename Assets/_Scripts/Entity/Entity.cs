using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Entity : MonoBehaviour
{
    public Cell occupiedCell;
    public StatsScriptable stats;

    public int currHealth;
    public int currShield = 0;
    public int currMovePoints;
    public int currAttackDamage;

    public StatusEffect currStatusEffect;

    public bool canPlay;

    public event Action OnDeath;

    public void Start()
    {
        currHealth = stats.HP;
        currMovePoints = stats.MOV;
        currAttackDamage = stats.ATK;
    }

    public void TakeDamage(int damage)
    {
        if (currShield > 0)
        {
            if (currShield >= damage) currShield -= damage;
            else
            {   
                currHealth -= damage - currShield;
                currShield = 0;
            }
        }

        currHealth -= damage;

        if (currHealth <= 0) DestroySelf();
    }

    public void DestroySelf()
    {
        occupiedCell.occupiedEntity = null;
        Destroy(this.gameObject);
        OnDeath?.Invoke();
    }
}
