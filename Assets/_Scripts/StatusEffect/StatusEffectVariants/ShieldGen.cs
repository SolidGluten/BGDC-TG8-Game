using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldGen", menuName = "ScriptableObjects/StatusEffect/ShieldGen")]
public class ShieldGen : Effect
{
    public int shieldGenPercentMultip = 25;

    public override void ApplyEffect(Entity caster, Entity target)
    {
    }

    public override void RemoveEffect(Entity caster, Entity target)
    {
        throw new System.NotImplementedException();
    }
}
