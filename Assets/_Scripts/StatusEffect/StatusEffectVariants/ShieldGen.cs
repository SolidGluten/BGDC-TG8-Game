using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldGen", menuName = "ScriptableObjects/StatusEffect/ShieldGen")]
public class ShieldGen : Effect
{
    public int shieldGenPercentMultip = 25;

    public override void ApplyEffect(Entity target, int effectMultip = 0)
    {

    }

    public override void RemoveEffect(Entity target)
    {
        throw new System.NotImplementedException();
    }
}
