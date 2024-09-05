using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingSalves", menuName = "ScriptableObjects/Cards/Healing Salves")]
public class HealingSalves : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        if (!target.Any()) return false;
        var targetList = target?.ToList();

        var ally = targetList.Select((target) => target.GetComponent<Character>())?.First();
        if (!ally) return false;

        ally.GainHealth(from.stats.ATK * (int)healMultiplier / 100);
        return true;
    }
}
