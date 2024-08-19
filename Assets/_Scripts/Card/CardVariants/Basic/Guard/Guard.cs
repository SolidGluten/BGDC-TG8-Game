using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Guard", menuName = "ScriptableObjects/Cards/Guard")]
public class Guard : Card
{
    float atkMultiplier = 225;
    public override bool Play(Entity from, Entity[] target)
    {
        if (target.Length > 0)
        {
            var targetList = target.ToList();
            var ally = targetList.Select((target) => target.GetComponent<Character>());

            if (!ally.Any()) return false;

            foreach (var character in ally)
            {
                character.GainShield(from.stats.ATK * (int)atkMultiplier / 100);
            }

            return true;
        }
        else return false;
    }
}
