using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public bool isActive;
    public StatsScriptable Stats;

    private void Start()
    {
        currHealth = Stats.HP;
        currAttackDamage = Stats.ATK;
    }

    public int currHealth = 0;
    public int currAttackDamage = 0;

    public event Action OnTakeDamage;
    public void TakeDamage(int dmg)
    {
        currHealth -= dmg;
        OnTakeDamage?.Invoke();
    }
}
