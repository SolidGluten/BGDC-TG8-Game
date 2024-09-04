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
            var characters = targetList.Select((target) => target.GetComponent<Character>()).ToList();
            characters.RemoveAll(x => x == null || x == from);

            if (!characters.Any()) return false;

            var ally = characters.First();
            ally.GainShield(from.stats.ATK * (int)atkMultiplier / 100);

            return true;
        }
        else return false;
    }
}
