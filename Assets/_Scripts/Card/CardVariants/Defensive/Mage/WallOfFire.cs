using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WallOfFire", menuName = "ScriptableObjects/Cards/Mage/Defensive/Wall Of Fire")]
public class WallOfFire : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var enemies = GetAllTargetEnemies(target);
        foreach (Enemy ene in enemies)
        {
            ApplyCardEffects(from, ene);
        }
        from.GainShield(from.stats.ATK * gainShieldMultiplier / 100);
        return true;
    }
}
