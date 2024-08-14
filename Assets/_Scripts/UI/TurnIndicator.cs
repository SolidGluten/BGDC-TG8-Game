using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnIndicator : MonoBehaviour
{
    public TextMeshProUGUI textMP;
    public TurnController turnController;

    private void Awake()
    {
        textMP = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textMP.text = "Turn : " + turnController.currTurnState.ToString();
    }

}
