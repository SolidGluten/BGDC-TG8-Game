using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovePointsIndicator : MonoBehaviour
{
    public TextMeshProUGUI moveIndicatorText;

    private void Awake()
    {
        if (!moveIndicatorText)
        {
            moveIndicatorText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        var chara = CharacterSelector.instance.SelectedCharacter;
        if (chara) {
            moveIndicatorText.text = "Move Left : " + chara.currMovePoints.ToString();
        };
    }
}
