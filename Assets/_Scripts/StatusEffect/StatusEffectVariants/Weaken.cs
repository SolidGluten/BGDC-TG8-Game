using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weaken", menuName = "ScriptableObjects/StatusEffect/Weaken")]
public class Weaken : Effect
{
    public int weakenPercentMultip = 25;

    public override void ApplyEffect(Entity target, int effectMultip = 0)
    {
        var weakenedDmg = target.currAttackDamage * weakenPercentMultip / 100;
        target.currAttackDamage -= weakenedDmg;
    }

    public override void RemoveEffect(Entity target)
    {
        var weakenedDmg = target.currAttackDamage * weakenPercentMultip / 100;
        target.currAttackDamage += weakenedDmg;
    }
}
