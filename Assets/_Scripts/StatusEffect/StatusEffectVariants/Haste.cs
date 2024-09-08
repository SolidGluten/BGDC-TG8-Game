using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Haste", menuName = "ScriptableObjects/StatusEffect/Haste")]
public class Haste : Effect
{
    public int addMovePoints = 2;
    public override void ApplyEffect(Entity target, int effectMultip = 0)
    {
        target.currMovePoints += Mathf.Max(0, addMovePoints);
    }

    public override void RemoveEffect(Entity target)
    {
        //throw new System.NotImplementedException();
    }
}
