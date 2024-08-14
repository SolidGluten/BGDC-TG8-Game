using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SolarForm", menuName = "ScriptableObjects/Cards/Solar Form")]
public class SolarForm : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
