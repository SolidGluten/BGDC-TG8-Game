using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sturdy", menuName = "ScriptableObjects/StatusEffect/Sturdy")]
public class Sturdy : Effect
{
    public int sturdyPercentMultip = 50;

    public override void ApplyEffect(Entity caster, Entity target)
    {
        target.shieldGainPercentMultip += sturdyPercentMultip;
    }

    public override void RemoveEffect(Entity caster, Entity target)
    {
        target.shieldGainPercentMultip -= sturdyPercentMultip; 
    }
}
