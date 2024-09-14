using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardDisplay))]
public class RewardCardPicker : MonoBehaviour, IPointerClickHandler
{
    private CardDisplay cardDisplay;

    private void Awake()
    {
        cardDisplay = GetComponent<CardDisplay>();  
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var rewardManager = (RewardManager)FindObjectOfType(typeof(RewardManager));
        if (rewardManager != null) rewardManager.PickCard(cardDisplay.CardInstance);
    }
}
