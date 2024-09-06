using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SolarForm", menuName = "ScriptableObjects/Cards/Solar Form")]
public class SolarForm : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var characters = GetAllTargetCharacters(target);
        foreach (Character chara in characters)
        {
            ApplyCardEffects(from, chara);
        }
        return true;
    }
}
