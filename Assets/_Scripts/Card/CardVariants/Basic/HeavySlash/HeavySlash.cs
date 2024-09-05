using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "HeavySlash", menuName = "ScriptableObjects/Cards/Heavy Slash")]
public class HeavySlash : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        if (target.Length > 0)
        {
            var targetList = target.ToList();
            var enemies = targetList.Select((target) => target.GetComponent<Enemy>()).ToList();
            enemies.RemoveAll(x => x == null);

            if (!enemies.Any()) return false;

            foreach (var enemy in enemies)
            {
                enemy.TakeDamage(from.stats.ATK * (int)dmgMultiplier / 100);
            }

            return true;
        }
        else return false;
    }
}
