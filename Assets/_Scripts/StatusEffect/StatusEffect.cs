using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffectType { 
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

[Serializable]
public class StatusEffect
{
    public int stacks;
    public Effect effect;
    public StatusEffectType type;

    public StatusEffect Clone()
    {
        return new StatusEffect
        {
            stacks = stacks,
            effect = effect,
            type = type
        };
    }
}
