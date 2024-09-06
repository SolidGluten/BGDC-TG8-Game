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
    DamageCardMultiplier
}

[Serializable]
public class StatusEffect
{
    public int stacks;
    public Effect effect;

    public Entity caster;
    public Entity target;

    private bool hasAppliedOnce = false;

    public StatusEffect(Effect effect, Entity caster, Entity target)
    {
        this.effect = effect;
        this.type = effect.type;
        this.stacks = effect.stacks;

        this.isStackable = effect.isStackable;
        this.isPermanent = effect.isPermanent;
        this.isAppliedOnStart = effect.isAppliedOnStart;
        this.isAppliedInstant = effect.isAppliedInstant;
        this.isReducedOnHit = effect.isReducedOnHit;
        this.isAppliedOnce = effect.isAppliedOnce;

        this.caster = caster;
        this.target = target;

        if (isAppliedInstant) UseEffect();

        if (isReducedOnHit) target.OnHit += UpdateEffect;
        else if (isAppliedOnStart) TurnController.instance.OnStartTurn += UpdateEffect;
        else TurnController.instance.OnEndTurn += UpdateEffect;
    }

    public event Action<StatusEffect> OnClearEffect;

    public StatusEffectType type { get; private set; }
    public bool isStackable { get; private set; }
    public bool isPermanent { get; private set; }
    public bool isAppliedOnStart { get; private set; }
    public bool isAppliedInstant { get; private set; }
    public bool isReducedOnHit { get; private set; }
    public bool isAppliedOnce { get; private set; }

    public void UseEffect()
    {
        hasAppliedOnce = true;
        effect.ApplyEffect(caster, target);
    }

    public void UpdateEffect()
    {
        if (isAppliedOnce)
        {
            if (!hasAppliedOnce) UseEffect();
        } else
        {
            // Apply effect again!
            UseEffect();
        }

        if (isPermanent) return;

        stacks -= 1;
        if (stacks <= 0)
        {
            ClearEffect(); //Remove Effect
            OnClearEffect?.Invoke(this);
        }
    }

    public void ClearEffect()
    {
        effect.RemoveEffect(caster, target);
        if (isAppliedOnStart) TurnController.instance.OnStartTurn -= UpdateEffect;
        else TurnController.instance.OnEndTurn -= UpdateEffect;
        if (isReducedOnHit) target.OnHit -= UpdateEffect;
    }
}
