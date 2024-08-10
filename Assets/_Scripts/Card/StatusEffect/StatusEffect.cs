using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffectType { 
    Stunned,
    Frozen,
    Burning,
    Weaken,
    Fragile,
    Fortified,
    Empowered,
    Sturdy,
    Buffer,
    Invincible,
    Slow,
    Haste,
    Regen,
    ShieldGen,
    Charged
}


public abstract class StatusEffect
{
    public abstract void ApplyEffect(Entity entity);
}
