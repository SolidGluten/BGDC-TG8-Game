using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Entity : MonoBehaviour
{
    private string _entityName;
    public string entityName {
        get { return _entityName; }
        set {
            _entityName = value;
            this.name = value;
        }
    }

    protected SpriteRenderer _spriteRenderer;

    public bool isFacingRight = false;

    public Cell occupiedCell;
    public StatsScriptable stats;

    public List<StatusEffect> appliedStatusEffects = new List<StatusEffect>();

    public int currHealth;
    public int currShield = 0;
    public int currMovePoints;
    public int currAttackDamage;

    public bool canMove = true;
    public bool canGainShield = true;
    public bool isInvincible = false;

    private bool isShieldPermanent = false;
    public bool IsShieldPermanent {
        get { return isShieldPermanent; }
        set {
            if (!isShieldPermanent) TurnController.instance.OnStartTurn += RemoveShield;
            else TurnController.instance.OnStartTurn -= RemoveShield;
        }
    }
        
    public event Action OnDeath;
    public event Action OnHit;

    public int dmgPercentMultip = 0;
    public int shieldGainPercentMultip = 0;

    public void Start()
    {
        currHealth = stats.HP;
        currMovePoints = stats.MOV;
        currAttackDamage = stats.ATK;

        IsShieldPermanent = false;

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Flip(bool toRight)
    {
        if (_spriteRenderer) {
            isFacingRight = toRight;
            _spriteRenderer.flipX = isFacingRight ? true : false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            damage = Math.Max(0, damage + damage * dmgPercentMultip / 100);

            if (currShield > 0)
            {
                if (currShield >= damage) currShield -= damage;
                else
                {   
                    currHealth -= damage - currShield;
                    currShield = 0;
                }
            } else
            {
                currHealth -= damage;
            }

            if (currHealth <= 0) DestroySelf();
        }

        OnHit?.Invoke();
    }
    public void GainShield(int shield)
    {
        if (!canGainShield) return;
        shield = Math.Max(0, shield + shield * shieldGainPercentMultip / 100);
        currShield += shield;
    }
    public void GainHealth(int heals)
    {
        currHealth += heals;

        if (currHealth > stats.HP)
        {
            currHealth = stats.HP;
        }
    }

    public void RemoveShield()
    {
        currShield = 0;
    }
    public void ApplyStatusEffect(Entity from, Effect effect)
    {
        var appEffect = appliedStatusEffects.Find((x) => x.effect == effect);
        if (appEffect != null)
        {
            if (!effect.isStackable) return;
            appEffect.stacks += effect.stacks;
        }
        else
        {
            var newEffect = new StatusEffect(effect, from, this);
            appliedStatusEffects.Add(newEffect);
            newEffect.OnClearEffect += RemoveStatusEffect;
        }
    }

    public StatusEffect GetStatusEffect(Effect effect)
    {
        return appliedStatusEffects.Find(x => x.effect == effect);
    }

    public StatusEffect[] GetDebuffs()
    {
        return appliedStatusEffects.Where(x => !x.effect.isBuff).ToArray();
    }

    public StatusEffect[] GetBuffs()
    {
        return appliedStatusEffects.Where(x => x.effect.isBuff).ToArray();
    }

    public void RemoveStatusEffect(StatusEffect effect) {
        var effectToRemove = appliedStatusEffects.Find(x => x == effect);
        appliedStatusEffects.Remove(effectToRemove);
    }

    public void DestroySelf()
    {
        occupiedCell.occupiedEntity = null;
        Destroy(this.gameObject);
        OnDeath?.Invoke();
    }
}