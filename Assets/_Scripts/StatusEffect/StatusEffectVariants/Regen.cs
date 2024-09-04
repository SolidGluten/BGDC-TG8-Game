using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Regen", menuName = "ScriptableObjects/StatusEffect/Regen")]
public class Regen : Effect
{
    public int regenPercentMultip = 25;

    public override void ApplyEffect(Entity caster, Entity target)
    {
        target.GainHealth(caster.stats.ATK * regenPercentMultip / 100);
    }

    public override void RemoveEffect(Entity caster, Entity target)
    {

    }
}
