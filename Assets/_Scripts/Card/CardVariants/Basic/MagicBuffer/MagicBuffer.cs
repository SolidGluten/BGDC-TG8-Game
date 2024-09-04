using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "MagicBuffer", menuName = "ScriptableObjects/Cards/MagicBuffer")]

public class MagicBuffer : Card
{
        public override bool Play(Entity from, Entity[] target)
    {
        if (target.Length > 0)
        {
            var targetList = target.ToList();
            var ally = targetList.Select((target) => target.GetComponent<Character>());

            if (!ally.Any()) return false;

            foreach (var character in ally)
            {
                character.ApplyStatusEffect(from, statusEffectToApply[0]);
                CardManager.instance.DrawCard();
            }

            return true;
        }
        else return false;
    }
}
