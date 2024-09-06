using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LunarForm", menuName = "ScriptableObjects/Cards/Lunar Form")]

public class LunarForm : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        foreach (var effect in statusEffectToApply) 
            from.ApplyStatusEffect(from, effect);
        return true;
    }
}
