using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingSalves", menuName = "ScriptableObjects/Cards/Healing Salves")]
public class HealingSalves : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
