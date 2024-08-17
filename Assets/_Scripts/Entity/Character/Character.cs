using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType { Knight, Mage };

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Character : Entity
{
    public bool isActive;
    public bool isTurn;

    public CharacterType type;

    public event Action OnTurnFinish;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && isActive && isTurn)
        {
            OnTurnFinish?.Invoke();
            isTurn = false;
        }
    }
}
