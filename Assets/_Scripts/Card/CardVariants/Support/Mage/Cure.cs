using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Cure", menuName = "ScriptableObjects/Cards/Mage/Support/Cure")]
public class Cure : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var characters = GetAllTargetCharacters(target);
        if (!characters.Any()) return false;

        foreach (Character chara in characters)
        {
            chara.RemoveDebuffs();
        }

        CardManager.instance.DrawCard();

        return true;
    }
}
