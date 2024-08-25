using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public int stacks;
    public StatusEffectType type;

    public bool isStackable;
    public bool isPermanent;
    public bool isAppliedOnStart;
    public bool isAppliedInstant;
    public bool isReducedOnHit;

    public abstract void ApplyEffect(Entity caster, Entity target);

    public abstract void RemoveEffect(Entity caster, Entity target);
}