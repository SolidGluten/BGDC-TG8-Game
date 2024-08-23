using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Entity : MonoBehaviour
{
    public string entityName;

    public Cell occupiedCell;
    public StatsScriptable stats;

    public int currHealth;
    public int currShield = 0;
    public int currMovePoints;
    public int currAttackDamage;

    public bool canMove = false;

    private StatusEffectType currStatusType;

    private StatusEffect currStatusEffect;
    public StatusEffect CurrStatusEffect { 
        get { return currStatusEffect; }
        set { currStatusEffect = value; }
    }

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
    public void GainShield(int shield)
    {
        //Reminder to add Sturdy
        currShield += shield;

    }
    public void GainHealth(int heals)
    {
        currHealth += heals;

        if (currHealth > stats.HP)
        {
            currHealth = stats.HP;
        }
    }
    public void DestroySelf()
    {
        occupiedCell.occupiedEntity = null;
        Destroy(this.gameObject);
        OnDeath?.Invoke();
    }
}
