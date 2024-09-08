using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fragile", menuName = "ScriptableObjects/StatusEffect/Fragile")]
public class Fragile : Effect
{
    public int fragilePercentMultip = 50;

    public override void ApplyEffect(Entity target, int effectMultip = 0)
    {
        target.dmgPercentMultip = fragilePercentMultip;
    }

    public override void RemoveEffect(Entity target)
    {
        target.dmgPercentMultip = 0;
    }
}
