using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrescentNight", menuName = "ScriptableObjects/Cards/Crescent Night")]
public class CrescentNight : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
