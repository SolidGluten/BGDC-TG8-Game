using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType { Knight, Mage };

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Character : Entity
{
    public bool isSelected;

    public CharacterType type;

    public event Action OnTurnFinish;

}
