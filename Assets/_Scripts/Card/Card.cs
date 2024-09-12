using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public CardRarity cardRarity;
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

    [Space(15)]
    public bool reduceCostOnTurn = false;
    public bool reduceCostOnMagicCard = false;
    public bool reduceCostOnKnightCard = false;

    [Space(15)]
    public bool addCostOnPlay = false;

    [Space(15)]
    public bool resetCostOnPlay = false;

    public virtual bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        return true;
    }

    public void ApplyCardEffects(Entity from, Entity target)
    {
        foreach (Effect effect in statusEffectToApply)
        {
            target.ApplyStatusEffect(from, effect);
        }
    }

    public List<Character> GetAllTargetCharacters(Entity[] entities, Character excludedCharacter = null)
    {
        var _entities = entities.ToList();
        var characters = _entities.Select((entity) => entity.GetComponent<Character>()).ToList();

        characters.RemoveAll(x => x == null);
        if (excludedCharacter) characters.Remove(excludedCharacter);

        return characters;
    }

    public List<Enemy> GetAllTargetEnemies(Entity[] entities, Enemy excludedEnemy = null)
    {
        var _entities = entities.ToList();
        var enemies = _entities.Select((entity) => entity.GetComponent<Enemy>()).ToList();

        enemies.RemoveAll(x => x == null);
        if (excludedEnemy) enemies.Remove(excludedEnemy);

        return enemies;
    }
}