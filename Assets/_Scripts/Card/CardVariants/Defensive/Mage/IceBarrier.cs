using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IceBarrier", menuName = "ScriptableObjects/Cards/Mage/Defensive/Ice Barrier")]
public class IceBarrier : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        ApplyCardEffects(from, from);
        CardManager.instance.DrawCard();
        return true;
    }
}
