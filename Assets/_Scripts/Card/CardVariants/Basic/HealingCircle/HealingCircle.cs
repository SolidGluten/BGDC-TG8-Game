using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingCircle", menuName = "ScriptableObjects/Cards/HealingCircle")]

public class HealingCircle : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
