using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "MagicShield", menuName = "ScriptableObjects/Cards/MagicShield")]
public class MagicShield : Card
{
    public float effectMultiplier = 150;
    public override bool Play(Entity from, Entity[] target)
    {
        if (target.Length > 0)
        {
            var targetList = target.ToList();
            var ally = targetList.Select((target) => target.GetComponent<Character>());

            if (!ally.Any()) return false;

            foreach (var character in ally)
            {
                character.GainShield(from.stats.ATK * (int)effectMultiplier / 100);
            }

            return true;
        }
        else return false;
    }
}