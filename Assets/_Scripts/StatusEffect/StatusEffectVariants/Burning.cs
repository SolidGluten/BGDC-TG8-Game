using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Burning", menuName = "ScriptableObjects/StatusEffect/Burning")] 
public class Burning : Effect
{
    public int burningPercentMultip = 25;

    public override void ApplyEffect(Entity caster, Entity target)
    {
        var burningDamage = caster.currAttackDamage * burningPercentMultip / 100;
        var effects = target.GetStatusEffect(this);
        var stacks = effects.stacks;

        target.TakeDamage(burningDamage * stacks);
    }

    public override void RemoveEffect(Entity caster, Entity target)
    {
        
    }
}
