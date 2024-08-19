using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BandageUp", menuName = "ScriptableObjects/Cards/Bandage Up")]
public class BandageUp : Card
{
    float atkMultiplier = 250;
    public override bool Play(Entity from, Entity[] target)
    {
        if (!target.Any()) return false;
        var targetList = target?.ToList();

        var ally = targetList.Select((target) => target.GetComponent<Character>())?.First();
        if (!ally) return false;

        ally.GainHealth(from.stats.ATK * (int)atkMultiplier / 100);
        return true;
    }
}
