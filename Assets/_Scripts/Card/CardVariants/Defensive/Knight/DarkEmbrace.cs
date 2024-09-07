using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DarkEmbrace", menuName = "ScriptableObjects/Cards/Dark Embrace")]

public class DarkEmbrace : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        ApplyCardEffects(from, from);
        from.canGainShield = false;
        return true;
    }
}
