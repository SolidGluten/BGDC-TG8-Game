using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UberCharged", menuName = "ScriptableObjects/Cards/UberCharged")]

public class UberCharged : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
