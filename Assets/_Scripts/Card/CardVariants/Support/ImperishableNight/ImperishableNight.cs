using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImperishableNight", menuName = "ScriptableObjects/Cards/Imperishable Night")]
public class ImperishableNight : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
