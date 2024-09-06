using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MoonsFavor", menuName = "ScriptableObjects/Cards/MoonsFavor")]

public class MoonsFavor : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        if (target.Length > 0)
        {
            var targetList = target.ToList();
            var ally = targetList.Select((target) => target.GetComponent<Character>());

            if (!ally.Any()) return false;

            foreach (var character in ally)
                foreach (var effect in statusEffectToApply)
                    character.ApplyStatusEffect(from, effect);

            return true;
        }
        else return false;
    }
}
