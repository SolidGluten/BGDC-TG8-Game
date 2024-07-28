using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeavySlash", menuName = "ScriptableObjects/Cards/HeavySlash")]
public class HeavySlash : Card
{
    public override void Play(Entity from, Entity target)
    {
        target.currHealth -= from.currAttackDamage * (225/100);
    }
}
