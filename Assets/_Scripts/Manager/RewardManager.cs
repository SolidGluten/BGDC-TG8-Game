using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RewardManager : MonoBehaviour
{
    public List<Card> randomizedCards = new List<Card>();

    private List<Card> rareCards = new List<Card>();
    private List<Card> uncommonCards = new List<Card>();
    private List<Card> commonCards = new List<Card>();

    [Space(15)]

    public Card temp;

    public CardDisplay firstRandomCard;
    public CardDisplay secondRandomCard;

    [Space(15)]

    public CardDisplay from;
    public CardDisplay to;

    [Space(15)]

    public RewardItem pickedReward;
    public CardInstance pickedCard;

    [Space(15)]

    public CharacterManager characterManager;

    public UnityEvent OnPickCard;

    Dictionary<CardRarity, int> rarityChances = new Dictionary<CardRarity, int>()
    {
        { CardRarity.Rare, 15 },
        { CardRarity.Uncommon, 30 },
        { CardRarity.Common, 45 },
    };

    private void Awake()
    {
        rareCards = CardManager.GetAllCardsByRarity(CardRarity.Rare);
        uncommonCards = CardManager.GetAllCardsByRarity(CardRarity.Uncommon);
        commonCards = CardManager.GetAllCardsByRarity(CardRarity.Common);
    }

    private void OnEnable()
    {
        characterManager.OnCharacterInitialize += InitializeRewards;
    }

    public void RandomCard()
    {
        var total = rarityChances.Aggregate(0, (acc, x) => acc + x.Value);
        var rand = UnityEngine.Random.Range(0, total);
    }

    public void PickReward(RewardItem reward)
    {
        pickedReward = reward;
    }

    public void PickCard(CardInstance cardInstance)
    {
        pickedCard = cardInstance;
        OnPickCard?.Invoke();
    }

    public void SetConfirmation()
    {
        if (pickedReward.type == RewardType.RandomCard)
        {
            // Replace card in deck to the random card
            from.CardInstance = pickedReward.cardDisplay.CardInstance;
            to.CardInstance = pickedCard;
        }
        else if (pickedReward.type == RewardType.Upgrade)
        {
            // Upgrade card
            Character chara = CharacterManager.instance.GetCharacterByType(temp.caster);
            CardInstance upgradedCardInstance = new CardInstance(pickedCard.cardScriptable.nextUpgrade, chara);
            from.CardInstance = pickedCard;
            to.CardInstance = upgradedCardInstance;
        }
    }

    public void ResetSelection()
    {
        pickedReward = null;
        pickedCard = null;
    }

    public void ApplyReward()
    {
        if (pickedReward.type == RewardType.RandomCard) {
            // Replace card in deck to the random card
            CardManager.instance.currentDeck.cards.Remove(pickedCard.cardScriptable);
            CardManager.instance.currentDeck.cards.Add(pickedReward.cardDisplay.CardInstance.cardScriptable);
        } else if(pickedReward.type == RewardType.Upgrade)
        {
            // Upgrade card
            var upgradedCard = pickedCard.cardScriptable.nextUpgrade;
            CardManager.instance.currentDeck.cards.Remove(pickedCard.cardScriptable);
            CardManager.instance.currentDeck.cards.Add(upgradedCard);
        }

        pickedReward.SetUse(true);
        ResetSelection();
    }

    private void InitializeRewards()
    {
        Character chara = CharacterManager.instance.GetCharacterByType(temp.caster);
        var cardInstance = new CardInstance(temp, chara);

        firstRandomCard.CardInstance = cardInstance;
        secondRandomCard.CardInstance = cardInstance;
    }

    private void OnDisable()
    {
        characterManager.OnCharacterInitialize -= InitializeRewards;
    }

}