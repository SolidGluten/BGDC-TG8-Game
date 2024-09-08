using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LastingShield", menuName = "ScriptableObjects/StatusEffect/LastingShield")]
public class LastingShield : Effect
{
    public override void ApplyEffect(Entity target, int effectMultip = 0)
    {
        target.IsShieldPermanent = true;
    }

    public override void RemoveEffect(Entity target)
    {
        target.IsShieldPermanent = false;
    }
}
