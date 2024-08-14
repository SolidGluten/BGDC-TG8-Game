using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Slash", menuName = "ScriptableObjects/Cards/Slash")]
public class Slash : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        var targetList = target.ToList();
        var enemy = targetList.Select((target) => target.GetComponent<Enemy>())?.First();
        if (!enemy) return false;

        enemy.TakeDamage(from.currAttackDamage);
        return true;
    }
}
