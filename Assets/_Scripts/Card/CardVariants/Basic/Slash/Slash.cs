using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slash", menuName = "ScriptableObjects/Cards/Slash")]
public class Slash : Card
{
    public override void Play(Entity from, Entity target)
    {
        target.currHealth -= from.currAttackDamage * (125 / 100);
    }
}
