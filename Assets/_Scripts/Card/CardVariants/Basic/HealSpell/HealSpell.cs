using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealSpell", menuName = "ScriptableObjects/Cards/HealSpell")]

public class HealSpell : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
