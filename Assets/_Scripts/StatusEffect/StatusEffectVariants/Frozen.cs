using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Frozen", menuName = "ScriptableObjects/StatusEffect/Frozen")]
public class Frozen : Effect
{
    public override void ApplyEffect(Entity target, int effectMultip = 0)
    {
        target.canMove = false;
    }

    public override void RemoveEffect(Entity target)
    {
        target.canMove = true;
    }
}
