using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Character : Entity, IDamageable
{
    public bool isActive;
    public bool isTurn;

    public event Action OnTakeDamage;
    public event Action OnTurnFinish;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && isActive && isTurn)
        {
            OnTurnFinish?.Invoke();
            isTurn = false;
        }
    }

    public void TakeDamage(int dmg)
    {
        currHealth -= dmg;
        OnTakeDamage?.Invoke();
    }
}
