using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicBolt", menuName = "ScriptableObjects/Cards/Magic Bolt")]
public class MagicBolt : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
