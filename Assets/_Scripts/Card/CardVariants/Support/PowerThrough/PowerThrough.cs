using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerThrough", menuName = "ScriptableObjects/Cards/PowerThrough")]
public class PowerThrough : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}