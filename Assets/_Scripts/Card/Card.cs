using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum CardType { Attack, Skill, Influence }
public enum CardType { Support, Defensive, Offensive, Basic}
public enum CardRarity { Common, Uncommon, Rare }

public abstract class Card : ScriptableObject
{
    public string cardName;
    [TextArea]
    public string description;
    public Sprite cardSprite;

    [Space(15)]
    public bool exhaust;
    public int cost;
    public CardType cardType;
    public CharacterType caster = CharacterType.Knight;

    [Space(15)]
    public int rangeFromCaster;
    public int range;
    public int width;
    public HighlightShape effectShape;
    public List<Effect> statusEffectToApply = new List<Effect>();

    [Space(15)]
    public int baseDamageMultiplier;
    public int baseHealMultiplier;
    public int baseGainShieldMultiplier;

    [Space(15)]
    public Card nextUpgrade;

    public virtual bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        return true;
    }
}