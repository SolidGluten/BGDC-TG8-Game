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


    public void TakeDamage(Entity from, Entity Target, int damage)
    {
        //if (Target.invincible)
        //{
        //    return;
        //}
        //else
        //{
        //    if (Target.currShield > 0)
        //    {
        //        if (Target.currShield >= from.currAttackDamage)
        //        {
        //            Target.currShield -= from.currAttackDamage;
        //        }
        //        else
        //        {
        //            Target.currHealth -= from.currAttackDamage - Target.currShield;
        //            Target.currShield = 0;
        //        }
        //    }
        //    Target.currHealth -= from.currAttackDamage;
        //}
    }

    public void DestroySelf()
    {
        occupiedCell.occupiedEntity = null;
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        OnDeath?.Invoke();
    }
}
