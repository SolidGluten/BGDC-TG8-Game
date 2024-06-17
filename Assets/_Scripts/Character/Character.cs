using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour, IDamageable
{
    public bool isActive;
    private StatsScriptable stats;
    public StatsScriptable Stats {
        get { return stats; }
        set { 
            stats = value;
            currHealth = stats.HP;
            currMovePoints = stats.MOV;
            currAttackDamage = stats.ATK;
        }
    }

    public event Action OnTakeDamage;
    public event Action OnTurnFinish;

    public int currHealth;
    public int currMovePoints;
    public int currAttackDamage;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && isActive)
        {
            OnTurnFinish?.Invoke();
        }
    }

    public void TakeDamage(int dmg)
    {
        currHealth -= dmg;
        OnTakeDamage?.Invoke();
    }
}
