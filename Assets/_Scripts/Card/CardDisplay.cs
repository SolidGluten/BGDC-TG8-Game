using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Canvas canvas;
    [HideInInspector] public int defaultSortOrder = 0;

    public TextMeshProUGUI card_name;
    public TextMeshProUGUI card_description;
    public TextMeshProUGUI card_cost;

    public Image card_image;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        card_image = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        card_name.text = card.cardName;
        card_description.text = card.description;
        card_cost.text = card.cost.ToString();
        card_image.sprite = card.cardSprite;

        ResetSortingOrder();
    }

    public void SetSortingOrder(int i)
    {
        canvas.sortingOrder = i;
    }

    public void ResetSortingOrder()
    {
        canvas.sortingOrder = defaultSortOrder;
    }
}
