using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RadiantBlessing", menuName = "ScriptableObjects/Cards/RadiantBlessing")]

public class RadiantBlessing : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        return true;
    }
}
