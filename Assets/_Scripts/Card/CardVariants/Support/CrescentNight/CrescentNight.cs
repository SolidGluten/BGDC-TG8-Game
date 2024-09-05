using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrescentNight", menuName = "ScriptableObjects/Cards/Crescent Night")]
public class CrescentNight : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        foreach(var effect in statusEffectToApply)
        {
            from.ApplyStatusEffect(from, effect);
        }
        return true;
    }
}
