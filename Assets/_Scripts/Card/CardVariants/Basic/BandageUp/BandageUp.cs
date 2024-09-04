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
        var targetList = target.ToList();
        var characters = targetList.Select((target) => target.GetComponent<Character>()).ToList();
        characters.RemoveAll(x => x == null || x == from);

        if (!characters.Any()) return false;

        var ally = characters.First();

        ally.GainHealth(from.stats.ATK * (int)atkMultiplier / 100);
        return true;
    }
}
