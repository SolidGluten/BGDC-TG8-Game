using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fortified", menuName = "ScriptableObjects/StatusEffect/Fortified")]
public class Fortified : Effect
{
    public int fortifiedPercentMultip = -50;

    public override void ApplyEffect(Entity caster, Entity target)
    {
        target.dmgPercentMultip = fortifiedPercentMultip;
    }

    public override void RemoveEffect(Entity caster, Entity target)
    {
        target.dmgPercentMultip = 0;
    }
}
