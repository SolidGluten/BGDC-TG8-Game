using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HoldShield", menuName = "ScriptableObjects/Cards/Hold Shield")]

public class HoldShield : Card
{
    float atkMultiplier = 175;
    public override bool Play(Entity from, Entity[] target)
    {
        from.GainShield(from.currAttackDamage * (int)atkMultiplier / 100);
        return true;
    }
}
