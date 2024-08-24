using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Slash", menuName = "ScriptableObjects/Cards/Slash")]
public class Slash : Card
{
    float atkMultiplier = 125;
    public override bool Play(Entity from, Entity[] target)
    {
        if (!target.Any()) return false;
        var targetList = target?.ToList();

        var enemy = targetList.Select((target) => target.GetComponent<Enemy>())?.First();
        if (!enemy) return false;

        foreach(var effect in statusEffectToApply)
        {
            enemy.ApplyStatusEffect(from, effect);
        }

        enemy.TakeDamage(from.currAttackDamage * (int)atkMultiplier/100);
        return true;
    }
}
