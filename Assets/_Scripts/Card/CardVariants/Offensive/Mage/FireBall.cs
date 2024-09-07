using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FireBall", menuName = "ScriptableObjects/Cards/Mage/Offensive/Fire Ball")]

public class FireBall : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var enemies = GetAllTargetEnemies(target);
        if (enemies.Any()) return false;

        foreach (var enemy in enemies)
        {
            ApplyCardEffects(from, enemy);
        }

        return true;
    }
}
