using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreshShield", menuName = "ScriptableObjects/Cards/Knight/Defensive/Fresh Shield")]

public class FreshShield : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        from.GainShield(from.stats.ATK * gainShieldMultiplier / 100);
        ApplyCardEffects(from, from);
        return true;
    }
}

