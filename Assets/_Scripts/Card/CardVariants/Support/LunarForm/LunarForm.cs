using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LunarForm", menuName = "ScriptableObjects/Cards/Lunar Form")]

public class LunarForm : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}