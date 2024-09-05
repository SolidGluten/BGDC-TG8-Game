using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "MagicShield", menuName = "ScriptableObjects/Cards/MagicShield")]
public class MagicShield : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        if (target.Length > 0)
        {
            var targetList = target.ToList();
            var characters = targetList.Select((target) => target.GetComponent<Character>()).ToList();
            characters.RemoveAll(x => x == null);

            if (!characters.Any()) return false;

            var ally = characters.First();
            ally.GainShield(from.stats.ATK * gainShieldMultiplier / 100);

            return true;
        }
        else return false;
    }
}