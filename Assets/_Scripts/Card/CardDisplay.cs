using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class CardDisplay : MonoBehaviour
{
    public CardInstance cardInstance;

    public Canvas canvas;
    [HideInInspector] public int defaultSortOrder = 0;

    public TextMeshProUGUI card_name;
    public TextMeshProUGUI card_description;
    public TextMeshProUGUI card_cost;

    public Image card_image;

    public static event Action OnRerenderAll;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        card_image = GetComponentInChildren<Image>();

        OnRerenderAll += Rerender;
    }

    private void Start()
    {
        Rerender();
    }

    public void SetSortingOrder(int i)
    {
        canvas.sortingOrder = i;
    }

    public void ResetSortingOrder()
    {
        canvas.sortingOrder = defaultSortOrder;
    }

    public void Rerender()
    {
        if (cardInstance == null)
        {
            Debug.LogWarning("card Instance is not set");
            return;
        }

        card_name.text = cardInstance.cardScriptable.name;
        card_description.text = cardInstance.cardScriptable.description;
        card_cost.text = cardInstance.cost.ToString();
        card_image.sprite = cardInstance.cardScriptable.cardSprite;

        ResetSortingOrder();
    }

    public void OnDestroy()
    {
        OnRerenderAll -= Rerender;
    }

    public static void RerenderAll()
    {
        OnRerenderAll?.Invoke();
    }
}
