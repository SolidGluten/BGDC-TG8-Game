using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleSlash", menuName = "ScriptableObjects/Cards/Double Slash")]
public class DoubleSlash : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        if (!target.Any()) return false;
        var targetList = target?.ToList();

        var enemy = targetList.Select((target) => target.GetComponent<Enemy>())?.First();
        if (!enemy) return false;

        enemy.TakeDamage(from.currAttackDamage);
        enemy.TakeDamage(from.currAttackDamage);
        return true;
    }
}
