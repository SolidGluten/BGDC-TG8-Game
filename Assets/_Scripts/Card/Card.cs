using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum CardType { Attack, Skill, Influence }
public enum CardType { Support, Defensive, Offensive, Basic}
public enum CardRarity { Common, Uncommon, Rare }

public abstract class Card : ScriptableObject
{
    public string cardName;
    public string description;
    public Sprite cardSprite;
    public Sprite cardBorder;
    public int cost;

    public CharacterType caster = CharacterType.Knight;

    public bool exhaust;

    public CardType cardType;

    public int rangeFromCaster;
    public int range;
    public int width;
    public HighlightShape effectShape;

    public List<Effect> statusEffectToApply = new List<Effect>();

    public virtual bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}