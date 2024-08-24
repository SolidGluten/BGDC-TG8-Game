using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public bool isStackable;
    public bool isPermanent;
    public bool isAppliedOnStart; //false => applied on end of turn

    public abstract void ApplyEffect(Entity caster, Entity target);

    public abstract void RemoveEffect(Entity caster, Entity target);
}
