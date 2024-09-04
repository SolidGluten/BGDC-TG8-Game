using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuickRest", menuName = "ScriptableObjects/Cards/Quick Rest")]
public class QuickRest : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        from.ApplyStatusEffect(from, statusEffectToApply[0]);
        from.ApplyStatusEffect(from, statusEffectToApply[1]);
        CardManager.instance.DrawCard();
        return true;
    }
}
