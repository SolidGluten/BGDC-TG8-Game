using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "MagicBarrier", menuName = "ScriptableObjects/Cards/MagicBarrier")]
public class MagicBarrier : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var characters = GetAllTargetCharacters(target);
        if (!characters.Any()) return false;

        foreach (var chara in characters)
        {
            chara.GainShield(from.stats.ATK * (int)gainShieldMultiplier / 100);
        }
        return true;
    }
}
