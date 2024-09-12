using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public List<Card> randomizedCards = new List<Card>();

    public List<Card> rareCards = new List<Card>();
    public List<Card> uncommonCards = new List<Card>();
    public List<Card> commonCards = new List<Card>();

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

    public void RandomizeCard()
    {
        var total = rarityChances.Aggregate(0, (acc, x) => acc + x.Value);
        var rand = Random.Range(0, total);

        
    }
}
