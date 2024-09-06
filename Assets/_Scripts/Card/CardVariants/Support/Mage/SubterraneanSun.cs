using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SubterraneanSun", menuName = "ScriptableObjects/Cards/Mage/Support/SubterraneanSun")]
public class SubterraneanSun : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var enemies = EnemyManager.Instance.ActiveEnemies;
        if (!enemies.Any()) return false;

        foreach (Enemy enemy in enemies)
        {
            ApplyCardEffects(from, enemy);
        }
        return true;
    }
}
