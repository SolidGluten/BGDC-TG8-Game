using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "IcicleFall", menuName = "ScriptableObjects/Cards/Mage/Offensive/Icicle Fall")]
public class IcicleFall : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var enemies = GetAllTargetEnemies(target);
        if (!enemies.Any()) return false;

        foreach (var enemy in enemies)
        {
            enemy.TakeDamage(from.stats.ATK * dmgMultiplier / 100);
        }

        foreach (var enemy in enemies)
        {
            ApplyCardEffects(from, enemy);
        }

        return true;
    }
}