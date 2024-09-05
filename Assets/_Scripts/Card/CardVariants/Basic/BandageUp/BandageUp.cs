using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BandageUp", menuName = "ScriptableObjects/Cards/Bandage Up")]
public class BandageUp : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var targetList = target.ToList();
        var characters = targetList.Select((target) => target.GetComponent<Character>()).ToList();
        characters.RemoveAll(x => x == null);

        if (!characters.Any()) return false;

        var ally = characters.First();

        ally.GainHealth(from.stats.ATK * (int)healMultiplier / 100);
        return true;
    }
}
