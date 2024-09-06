using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MagesMercy", menuName = "ScriptableObjects/StatusEffect/MagesMercy")]
public class MagesMercy : Effect
{
    public override void ApplyEffect(Entity caster, Entity target)
    {
        var allMageCards = CardManager.instance.GetAllPlayingCards().Where(x => x.cardScriptable.caster == CharacterType.Mage);

        foreach (var card in allMageCards)
        {
            card.ReduceCost(1);
        }
        CardDisplay.RerenderAll();
    }

    public override void RemoveEffect(Entity caster, Entity target)
    {
        var allMageCards = CardManager.instance.GetAllPlayingCards().Where(x => x.cardScriptable.caster == CharacterType.Mage);

        foreach (var card in allMageCards)
        {
            card.AddCost(1);
        }
        CardDisplay.RerenderAll();
    }
}
