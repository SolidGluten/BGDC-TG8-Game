using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "MagicBullet", menuName = "ScriptableObjects/Cards/MagicBullet")]
public class MagicBullet : Card
{
    float atkMultiplier = 125;
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        if (target.Length > 0)
        {
            var targetList = target.ToList();
            var enemies = targetList.Select((target) => target.GetComponent<Enemy>());

            if (!enemies.Any()) return false;

            foreach (var enemy in enemies)
            {
                enemy.TakeDamage(from.stats.ATK * (int)atkMultiplier/125);
            }

            return true;
        }
        else return false;
    }
}
