using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleSlash", menuName = "ScriptableObjects/Cards/Double Slash")]
public class DoubleSlash : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
