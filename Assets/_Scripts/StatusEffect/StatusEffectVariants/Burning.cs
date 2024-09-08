using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Burning", menuName = "ScriptableObjects/StatusEffect/Burning")] 
public class Burning : Effect
{
    public int burningPercentMultip = 25;

    public override void ApplyEffect(Entity target, int effectMultip = 0)
    {
        var burningDamage = effectMultip * burningPercentMultip / 100;
        var effects = target.GetStatusEffect(this);
        var stacks = effects.stacks;

        target.TakeDamage(burningDamage * stacks);
    }

    public override void RemoveEffect(Entity target)
    {
        
    }
}
