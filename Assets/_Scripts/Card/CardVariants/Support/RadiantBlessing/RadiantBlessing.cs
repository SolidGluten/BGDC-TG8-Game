using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RadiantBlessing", menuName = "ScriptableObjects/Cards/RadiantBlessing")]

public class RadiantBlessing : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
