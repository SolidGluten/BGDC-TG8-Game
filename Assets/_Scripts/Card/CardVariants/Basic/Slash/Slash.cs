using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slash", menuName = "ScriptableObjects/Cards/Slash")]
public class Slash : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
