using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageCardMultip", menuName = "ScriptableObjects/StatusEffect/DamageCardMultip")]
public class DamageCardMultiplier : Effect
{
    public int dmgMultiplier = 2;
    public override void ApplyEffect(Entity target, int effectMultip = 0)
    {
        var cards = CardManager.instance.GetAllPlayingCards();
        foreach (var card in cards)
        {
            card.cardDamageMultiplier *= dmgMultiplier;
        }
    }

    public override void RemoveEffect(Entity target)
    {
        var cards = CardManager.instance.GetAllPlayingCards();
        foreach (var card in cards)
        {
            card.cardDamageMultiplier /= dmgMultiplier;
        }
    }
}
