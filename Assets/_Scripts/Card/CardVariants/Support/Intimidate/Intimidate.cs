using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Intimidate", menuName = "ScriptableObjects/Cards/Intimidate")]
public class Intimidate : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        ApplyCardEffects(from, from);
        return true;
    }
}
