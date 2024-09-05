using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GripShield", menuName = "ScriptableObjects/Cards/Grip Shield")]
public class GripShield : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        foreach(Effect effect in statusEffectToApply)
            from.ApplyStatusEffect(from, effect);
        CardManager.instance.DrawCard();
        return true;
    }
}
