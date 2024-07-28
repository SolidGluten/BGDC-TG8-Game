using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, IDamageable
{
    public bool isActive;

    public event Action OnTakeDamage;
    public void TakeDamage(int dmg)
    {
        currHealth -= dmg;
        OnTakeDamage?.Invoke();
    }
}
