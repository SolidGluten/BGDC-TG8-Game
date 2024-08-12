using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class CardDisplay : MonoBehaviour
{
    public Card card;

    public TextMeshProUGUI card_name;
    public TextMeshProUGUI card_description;
    public TextMeshProUGUI card_cost;

    private void Start()
    {
        card_name.text = card.cardName;
        card_description.text = card.description;
        card_cost.text = card.cost.ToString();
    }
}
