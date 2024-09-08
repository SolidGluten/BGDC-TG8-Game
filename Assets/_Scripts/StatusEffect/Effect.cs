using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public int stacks;
    public StatusEffectType type;
    public bool isBuff = true; // false => Debuff

    [Space(15)]
    public bool isStackable;
    public bool newInstance_OnStack;
    public bool increaseDuration_OnStack;
    public bool increaseEffectMultip_OnStack;

    [Space(15)]
    public bool isAppliedOnStart; // false => applied on end of turn
    public bool isAppliedInstant;

    [Space(15)]
    public bool isReducedOnHit;
    public bool isAppliedOnce; // false => applied on every turn 
    public bool isPermanent;

    [Space(15)]
    public bool takeDmgAsEffectMultip;

    public abstract void ApplyEffect(Entity target, int effectMultip = 0);

    public abstract void RemoveEffect(Entity target);
}
