using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "Purification", menuName = "ScriptableObjects/Cards/Mage/Support/Purification")]
public class Purification : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var characters = GetAllTargetCharacters(target);
        if (!characters.Any()) return false;

        foreach (Character chara in characters)
        {
            chara.RemoveDebuffs();
        }

        for(int i = 0; i < 2; i++)
        {
            CardManager.instance.DrawCard();
        }

        return true;
    }
}
