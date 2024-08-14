using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RegenSpell", menuName = "ScriptableObjects/Cards/RegenSpell")]
public class RegenSpell : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
