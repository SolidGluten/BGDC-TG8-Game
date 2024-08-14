using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bloodletting", menuName = "ScriptableObjects/Cards/Bloodletting")]
public class Bloodletting : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
