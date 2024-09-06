using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Bloodletting", menuName = "ScriptableObjects/Cards/Bloodletting")]
public class Bloodletting : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        from.GainHealth(from.stats.ATK * healMultiplier / 100);
        ApplyCardEffects(from, from);

        var debuffs = from.GetDebuffs();
        for(int i = 0; i < debuffs.Length; i++)
        {
            from.RemoveStatusEffect(debuffs[i]);
        }

        for(int i = 0; i < 2; i++)
        {
            CardManager.instance.DrawCard();
        }

        return true;
    }
}
