using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : ScriptableObject
{
    public string cardName;
    public string cardDesc;
    public int cost;
    public Sprite cardImage;
    public Sprite cardBorder;
    public bool exhaust = false;
    public enum CardType
    {
        Attack, Skill, Influence
    }
    public CardType cardType;
    public virtual void cardEffect()
    {

    }
}
