using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LastingShield", menuName = "ScriptableObjects/StatusEffect/LastingShield")]
public class LastingShield : Effect
{
    public override void ApplyEffect(Entity caster, Entity target)
    {
        target.IsShieldPermanent = true;
    }

    public override void RemoveEffect(Entity caster, Entity target)
    {
        target.IsShieldPermanent = false;
    }
}
