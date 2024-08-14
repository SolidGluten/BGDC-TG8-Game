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
            var enemies = targetList.Select((target) => target.GetComponent<Enemy>());

            if (!enemies.Any()) return false;

            foreach(var enemy in enemies)
            {
                enemy.TakeDamage(from.stats.ATK);
            }

            return true;
        }
        else return false;
    }
}
