using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BandageUp", menuName = "ScriptableObjects/Cards/Bandage Up")]
public class BandageUp : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}