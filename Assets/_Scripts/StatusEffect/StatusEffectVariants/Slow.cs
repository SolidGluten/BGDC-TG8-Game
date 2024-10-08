using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slow", menuName = "ScriptableObjects/StatusEffect/Slow")]
public class Slow : Effect
{
    public int reduceMovePoints = 2;

    public override void ApplyEffect(Entity target, int effectMultip = 0)
    {
        target.currMovePoints -= reduceMovePoints;
    }

    public override void RemoveEffect(Entity target)
    {

    }
}
