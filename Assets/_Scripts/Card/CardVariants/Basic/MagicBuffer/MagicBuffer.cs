using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicBuffer", menuName = "ScriptableObjects/Cards/MagicBuffer")]

public class MagicBuffer : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
