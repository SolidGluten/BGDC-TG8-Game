using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezeAtmosphere", menuName = "ScriptableObjects/Cards/Mage/Defensive/Freeze Atmosphere")]
public class FreezeAtmosphere : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        ApplyCardEffects(from, from);
        return true;
    }
}
