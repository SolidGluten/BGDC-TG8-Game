using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fortify", menuName = "ScriptableObjects/Cards/Knight/Defensive/Fortify")]

public class Fortify : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        from.currShield = from.currShield * gainShieldMultiplier / 100;
        ApplyCardEffects(from, from);
        return true;
    }
}

