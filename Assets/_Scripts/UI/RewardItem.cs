using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum RewardType { Upgrade, RandomCard }

public class RewardItem : MonoBehaviour
{
    public RewardType type = RewardType.RandomCard;

    public CardDisplay cardDisplay;
    public bool isUsed = false;

    public Button button;

    private void Start()
    {
        cardDisplay = GetComponent<CardDisplay>();
        button = GetComponent<Button>();
    }

    public void SetUse(bool used)
    {
        this.gameObject.SetActive(used ? false : true);
        button.interactable = used ? false : true;
    }
}
