using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Invincible", menuName = "ScriptableObjects/StatusEffect/Invincible")]
public class Invincible : Effect
{
    public override void ApplyEffect(Entity target, int effectMultip = 0)
    {
        target.isInvincible = true;
    }

    public override void RemoveEffect(Entity target)
    {
        target.isInvincible = false;
    }
}
