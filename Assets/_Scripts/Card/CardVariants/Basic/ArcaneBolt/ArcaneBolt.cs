using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArcaneBolt", menuName = "ScriptableObjects/Cards/ArcaneBolt")]
public class ArcaneBolt : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
