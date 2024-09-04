using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Slash", menuName = "ScriptableObjects/Cards/Slash")]
public class Slash : Card
{
    public float atkMultiplier = 125;
    public override bool Play(Entity from, Entity[] target)
    {
        if (!target.Any()) return false;
        var targetList = target?.ToList();

        var enemies = targetList.Select((target) => target.GetComponent<Enemy>()).ToList();
        enemies.RemoveAll(x => x == null);
        var enemy = enemies.First();

        if (!enemy) return false;

        enemy.TakeDamage(from.currAttackDamage * (int)atkMultiplier/100);

        if (enemy)
        {
            foreach (var effect in statusEffectToApply)
            {
                enemy.ApplyStatusEffect(from, effect);
            }
        }

        return true;
    }
}
