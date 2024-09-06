using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlackMist", menuName = "ScriptableObjects/Cards/Black Mist")]

public class BlackMist : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        for (int i = 0; i < statusEffectToApply.Count; i++)
        {
            var effect = statusEffectToApply[i];
            if (effect) from.ApplyStatusEffect(from, effect);
        }
        return true;
    }
}
