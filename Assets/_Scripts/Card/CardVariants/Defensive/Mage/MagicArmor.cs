using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicArmor", menuName = "ScriptableObjects/Cards/Mage/Defensive/MagicArmor")]

public class MagicArmor: Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var enemies = GetAllTargetEnemies(target);
        foreach (Enemy ene in enemies)
        {
            from.GainShield(from.stats.ATK * gainShieldMultiplier / 100);
        }
        return true;
    }
}
