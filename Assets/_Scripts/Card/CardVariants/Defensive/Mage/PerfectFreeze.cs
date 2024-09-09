using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PerfectFreeze", menuName = "ScriptableObjects/Cards/Mage/Defensive/Perfect Freeze")]
public class PerfectFreeze : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var characters = CharacterManager.instance.ActiveCharacters;

        foreach (Character chara in characters)
        {
            ApplyCardEffects(from, chara);
        }
        return true;
    }
}
