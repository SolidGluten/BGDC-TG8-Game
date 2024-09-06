using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerThrough", menuName = "ScriptableObjects/Cards/PowerThrough")]
public class PowerThrough : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        from.GainHealth(from.stats.ATK * healMultiplier / 100);
        ApplyCardEffects(from, from);

        return true;
    }
}
