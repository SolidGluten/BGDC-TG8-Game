using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum CardType { Attack, Skill, Influence }
public enum CardType { Support, Defensive, Offensive}
public enum CardRarity { Common, Uncommon, Rare }

public abstract class Card : ScriptableObject
{
    public string cardName;
    public string description;
    public CardType cardType;
    public int cost;
    public int range;
    public int width;
    public HighlightShape effectShape;
    public Sprite cardSprite;
    public Sprite cardBorder;
    public bool exhaust;
    public StatusEffect statusEffect;

    public abstract void Play(Entity from, Entity target);
}
