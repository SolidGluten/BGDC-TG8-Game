using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicAbsorber", menuName = "ScriptableObjects/Cards/Mage/Defensive/Magic Absorber")]

public class MagicAbsorber : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var enemies = GetAllTargetEnemies(target);
        foreach (Enemy ene in enemies)
        {
            ene.TakeDamage(from.stats.ATK * dmgMultiplier / 100);
            from.GainShield(from.stats.ATK * gainShieldMultiplier / 100);
        }
        return true;
    }
}
