using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    public override bool ValidateMove(Cell cell)
    {
        return true;
    }
}
