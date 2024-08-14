using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Guard", menuName = "ScriptableObjects/Cards/Guard")]
public class Guard : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
