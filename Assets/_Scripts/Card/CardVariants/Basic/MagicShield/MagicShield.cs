using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicShield", menuName = "ScriptableObjects/Cards/MagicShield")]
public class MagicShield : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
