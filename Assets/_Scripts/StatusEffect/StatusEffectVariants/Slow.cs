using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slow", menuName = "ScriptableObjects/StatusEffect/Slow")]
public class Slow : Effect
{
    public int slowPercentMultip = 25;

    public override void ApplyEffect(Entity caster, Entity target)
    {
        target.currMovePoints -= Mathf.Max(0, caster.stats.ATK * slowPercentMultip / 100);
    }

    public override void RemoveEffect(Entity caster, Entity target)
    {

    }
}
