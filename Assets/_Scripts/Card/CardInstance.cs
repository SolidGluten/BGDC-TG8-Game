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
    public float cardDamageMultiplier;
    public float cardHealMultiplier;

    //public bool isUpgradable = false;

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
        //isUpgradable = cardScriptable.nextUpgrade != null;
    }

    public bool PlayCard(Entity[] target)
    {
        if(!(cardScriptable || caster))
        {
            Debug.LogWarning("Caster or Card has not been asigned.");
            return false;
        }

        return cardScriptable.Play(caster, target);
    }

    //public void UpgradeCard()
    //{
    //    if(isUpgradable) SetCard(cardScriptable.nextUpgrade);
    //}
}
