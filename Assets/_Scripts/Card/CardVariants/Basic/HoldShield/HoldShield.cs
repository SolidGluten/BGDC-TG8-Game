using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HoldShield", menuName = "ScriptableObjects/Cards/Hold Shield")]

public class HoldShield : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        from.GainShield(from.stats.ATK * (int)gainShieldMultiplier / 100);
        return true;
    }
}
