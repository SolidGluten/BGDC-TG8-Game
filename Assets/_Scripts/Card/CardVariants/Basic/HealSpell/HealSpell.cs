using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "HealSpell", menuName = "ScriptableObjects/Cards/HealSpell")]

public class HealSpell : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        if (target.Length > 0)
        {
            var targetList = target.ToList();
            var allies = targetList.Select((target) => target.GetComponent<Character>());

            if (!allies.Any()) return false;

            foreach (var ally in allies)
            {
                ally.GainHealth(from.stats.ATK);
            }

            return true;
        }
        else return false;
    }
}
