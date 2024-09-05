using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleSlash", menuName = "ScriptableObjects/Cards/Double Slash")]
public class DoubleSlash : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        if (!target.Any()) return false;
        var targetList = target?.ToList();

        var enemy = targetList.Select((target) => target.GetComponent<Enemy>())?.First();
        if (!enemy) return false;

        enemy.TakeDamage(from.currAttackDamage * dmgMultiplier / 100);
        enemy.TakeDamage(from.currAttackDamage * dmgMultiplier / 100);
        return true;
    }
}
