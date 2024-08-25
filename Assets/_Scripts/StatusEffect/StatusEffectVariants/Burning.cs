using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Burning", menuName = "ScriptableObjects/StatusEffect/Frozen")]
public class Burning : Effect
{
    public override void ApplyEffect(Entity caster, Entity target)
    {
        
    }

    public override void RemoveEffect(Entity caster, Entity target)
    {
        throw new System.NotImplementedException();
    }
}
