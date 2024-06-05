using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Character : Entity
{
    [SerializeField] private bool isCharSelected;
    [SerializeField] private float moveRange;

    public override bool ValidateMove(Cell cell)
    {
        return true;
    }
}
