using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weaken", menuName = "ScriptableObjects/StatusEffect/Weaken")]
public class Weaken : Effect
{
    public int weakenPercentMultip = 25;

    public override void ApplyEffect(Entity caster, Entity target)
    {
        target.currAttackDamage = target.stats.ATK * weakenPercentMultip / 100;
    }

    public override void RemoveEffect(Entity caster, Entity target)
    {
        target.currAttackDamage = target.stats.ATK;
    }
}
