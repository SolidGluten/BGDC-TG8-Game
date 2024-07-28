using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleSlash", menuName = "ScriptableObjects/Cards/Double Slash")]
public class DoubleSlash : Card
{
    public override void Play(Entity from, Entity target)
    {
        target.currHealth -= from.currAttackDamage;
        target.currHealth -= from.currAttackDamage;
    }
}
