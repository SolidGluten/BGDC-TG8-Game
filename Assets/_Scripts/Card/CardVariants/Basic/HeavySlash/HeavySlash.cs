using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeavySlash", menuName = "ScriptableObjects/Cards/Heavy Slash")]
public class HeavySlash : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
