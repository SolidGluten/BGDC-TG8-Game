using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelenesProtection", menuName = "ScriptableObjects/Cards/Selenes Protection")]

public class SelenesProtection : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
