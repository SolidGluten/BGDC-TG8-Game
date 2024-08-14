using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HoldShield", menuName = "ScriptableObjects/Cards/Hold Shield")]

public class HoldShield : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
