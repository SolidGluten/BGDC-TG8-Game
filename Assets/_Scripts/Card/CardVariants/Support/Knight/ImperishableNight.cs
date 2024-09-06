using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImperishableNight", menuName = "ScriptableObjects/Cards/Imperishable Night")]
public class ImperishableNight : Card
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
