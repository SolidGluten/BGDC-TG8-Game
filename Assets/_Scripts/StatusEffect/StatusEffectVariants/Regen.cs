using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Regen", menuName = "ScriptableObjects/StatusEffect/Regen")]
public class Regen : Effect
{
    public int regenPercentMultip = 25;

    public override void ApplyEffect(Entity target, int effectMultip = 0)
    {
        target.GainHealth(effectMultip * regenPercentMultip / 100);
    }

    public override void RemoveEffect(Entity target)
    {

    }
}
