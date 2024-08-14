using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoonsFavor", menuName = "ScriptableObjects/Cards/MoonsFavor")]

public class MoonsFavor : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
