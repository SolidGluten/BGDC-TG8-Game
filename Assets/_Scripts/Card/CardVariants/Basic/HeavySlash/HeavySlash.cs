using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "HeavySlash", menuName = "ScriptableObjects/Cards/Heavy Slash")]
public class HeavySlash : Card
{
    float atkMultiplier = 225;
    public override bool Play(Entity from, Entity[] target)
    {
        if (target.Length > 0)
        {
            var targetList = target.ToList();
            var enemies = targetList.Select((target) => target.GetComponent<Enemy>());

            if (!enemies.Any()) return false;

            foreach (var enemy in enemies)
            {
                enemy.TakeDamage(from.stats.ATK * (int)atkMultiplier / 100);
            }

            return true;
        }
        else return false;
    }
}
