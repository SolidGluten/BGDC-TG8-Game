using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    public bool isActive;
    public bool isSelected;

    public StatsScriptable Stats;

    public int currHealth;
    public int currMovePoints;
    public int currAttackDamage;

    
}
