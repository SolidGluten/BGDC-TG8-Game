using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "HealingCircle", menuName = "ScriptableObjects/Cards/HealingCircle")]

public class HealingCircle : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        if (target.Length > 0)
        {
            var targetList = target.ToList();
            var allies = targetList.Select((target) => target.GetComponent<Character>()).ToList();
            allies.RemoveAll(x => x == null);


            if (!allies.Any()) return false;

            foreach (var ally in allies)
            {
                ally.GainHealth(from.stats.ATK * healMultiplier / 100);
            }
            CardManager.instance.DrawCard();
            return true;
        }
        else return false;
    }
}
