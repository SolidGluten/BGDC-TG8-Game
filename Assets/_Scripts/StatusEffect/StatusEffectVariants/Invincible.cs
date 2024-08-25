using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Invincible", menuName = "ScriptableObjects/StatusEffect/Invincible")]
public class Invincible : Effect
{
    public override void ApplyEffect(Entity caster, Entity target)
    {
        target.isInvincible = true;
    }

    public override void RemoveEffect(Entity caster, Entity target)
    {
        target.isInvincible = false;
    }
}
