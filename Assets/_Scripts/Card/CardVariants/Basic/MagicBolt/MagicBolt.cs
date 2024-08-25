using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicBolt", menuName = "ScriptableObjects/Cards/Magic Bolt")]
public class MagicBolt : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        if (target.Length > 0)
        {
            var targetList = target.ToList();
            var enemies = targetList.Select((target) => target.GetComponent<Enemy>()).ToList();
            enemies.RemoveAll(x => x == null);

            if (!enemies.Any()) return false;

            foreach(var enemy in enemies)
            {
                enemy.TakeDamage(from.stats.ATK);
                foreach(var effects in statusEffectToApply)
                {
                    enemy.ApplyStatusEffect(from, effects);
                }
            }

            return true;
        }
        else return false;
    }
}
