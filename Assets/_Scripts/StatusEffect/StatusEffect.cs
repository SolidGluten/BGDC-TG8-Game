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
    Charged,
    DamageCardMultiplier,
    LastingShield,
    MagesMercy
}

[Serializable]
public class StatusEffect
{
    public int stacks;
    public int effectMultiplier = 0;
    public Effect effect;

    //public Entity caster;
    public Entity target;

    private bool hasAppliedOnce = false;

    public StatusEffect(Effect effect, Entity caster, Entity target)
    {
        this.effect = effect;
        this.stacks = effect.stacks;

        this.target = target;

        if (effect.takeDmgAsEffectMultip) effectMultiplier = caster.stats.ATK;
        if (effect.isAppliedInstant) UseEffect();

        if (effect.isReducedOnHit) target.OnHit += UpdateEffect;
        else if (effect.isAppliedOnStart) TurnController.instance.OnStartTurn += UpdateEffect;
        else TurnController.instance.OnEndTurn += UpdateEffect;
    }

    public event Action<StatusEffect> OnClearEffect;

    public void UseEffect()
    {
        hasAppliedOnce = true;
        effect.ApplyEffect(target, effectMultiplier);
    }

    public void UpdateEffect()
    {
        if (effect.isAppliedOnce)
        {
            if (!hasAppliedOnce) UseEffect();
        } else
        {
            // Apply effect again!
            UseEffect();
        }

        if (effect.isPermanent) return;

        stacks -= 1;
        if (stacks < 0)
        {
            ClearEffect(); //Remove Effect
            OnClearEffect?.Invoke(this);
        }
    }

    public void ClearEffect()
    {
        effect.RemoveEffect(target);
        if (effect.isAppliedOnStart) TurnController.instance.OnStartTurn -= UpdateEffect;
        else TurnController.instance.OnEndTurn -= UpdateEffect;
        if (effect.isReducedOnHit) target.OnHit -= UpdateEffect;
    }
}
