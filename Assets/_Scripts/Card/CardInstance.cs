using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardInstance
{
    public Card cardScriptable { get; private set; }
    public Character caster { get; private set; }

    public int cost;
    public int cardDamageMultiplier;
    public int cardHealMultiplier;
    public int cardGainShieldMultiplier;

    public bool isUpgradable = false;

    public CardInstance(Card _card, Character _caster)
    {
        SetCard(_card);
        caster = _caster;
    }

    public void SetCard(Card card)
    {
        cardScriptable = card;
        cost = cardScriptable.cost;
        cardDamageMultiplier = cardScriptable.baseDamageMultiplier;
        cardHealMultiplier = cardScriptable.baseHealMultiplier;
        cardGainShieldMultiplier = cardScriptable.baseGainShieldMultiplier;
        isUpgradable = cardScriptable.nextUpgrade != null && cardScriptable.cardType != CardType.Basic;

        if (card.reduceCostOnTurn) TurnController.instance.OnEndTurn += ReduceCostOne;
        if (card.reduceCostOnMagicCard) CardManager.instance.OnPlayCard += ReduceCostOnMagicCard;
        if (card.reduceCostOnKnightCard) CardManager.instance.OnPlayCard += ReduceCostOnKnightCard;
    }

    public bool PlayCard(Entity[] target)
    {
        if(!(cardScriptable || caster))
        {
            Debug.LogWarning("Caster or Card has not been asigned.");
            return false;
        }

        if (cardScriptable.Play(caster, target, cardDamageMultiplier, cardHealMultiplier, cardGainShieldMultiplier))
        {
            if (cardScriptable.resetCostOnPlay)
                cost = cardScriptable.cost;
            if (cardScriptable.addCostOnPlay)
                AddCost(1);
            return true;
        } else
        {
            return false;
        }

    }

    public void ReduceCostOne()
    {
        ReduceCost(1);
    }

    public void ReduceCost(int _cost)
    {
        cost = Math.Max(0, cost - _cost);
        CardDisplay.RerenderAll();
    }

    public void ReduceCostOnMagicCard(CardInstance card)
    {
        if (card == null) return;
        if (card.cardScriptable.caster == CharacterType.Mage) ReduceCost(1);
    }

    public void ReduceCostOnKnightCard(CardInstance card)
    {
        if (card == null) return;
        if (card.cardScriptable.caster == CharacterType.Knight) ReduceCost(1);
    }

    public void AddCost(int _cost)
    {
        cost += _cost;
        CardDisplay.RerenderAll();
    }

    public void UpgradeCard()
    {
        if (isUpgradable) SetCard(cardScriptable.nextUpgrade);
    }
}
