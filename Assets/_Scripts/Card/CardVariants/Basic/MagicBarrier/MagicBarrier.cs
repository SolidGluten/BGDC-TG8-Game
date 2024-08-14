using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicBarrier", menuName = "ScriptableObjects/Cards/MagicBarrier")]
public class MagicBarrier : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
