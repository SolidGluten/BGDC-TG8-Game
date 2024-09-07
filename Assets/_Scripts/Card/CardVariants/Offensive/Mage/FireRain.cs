using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireRain", menuName = "ScriptableObjects/Cards/Mage/Offensive/Fire Rain")]
public class FireRain : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        return false;
    }
}
