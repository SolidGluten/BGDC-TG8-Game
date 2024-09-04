using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "RegenSpell", menuName = "ScriptableObjects/Cards/RegenSpell")]
public class RegenSpell : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        if (target.Length > 0)
        {
            var targetList = target.ToList();
            var allies = targetList.Select((target) => target.GetComponent<Character>());

            if (!allies.Any()) return false;

            foreach (var ally in allies)
            {
                ally.ApplyStatusEffect(from, statusEffectToApply[0]);
            }

            return true;
        }
        else return false;
    }
}
