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

    public Cell occupiedCell;
    public StatsScriptable stats;

    public List<StatusEffect> appliedStatusEffects = new List<StatusEffect>();

    public int currHealth;
    public int currShield = 0;
    public int currMovePoints;
    public int currAttackDamage;

    public bool canMove = true;

    public bool canPlay;

    public event Action OnDeath;
    public event Action OnDamage;

    public int dmgPercentMultip = 0;
    public int shieldGainPercentMultip = 0;

    public void Start()
    {
        currHealth = stats.HP;
        currMovePoints = stats.MOV;
        currAttackDamage = stats.ATK;
    }

    public void TakeDamage(int damage)
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
        }

        currHealth -= damage;
        OnDamage?.Invoke();

        if (currHealth <= 0) DestroySelf();
    }
    public void GainShield(int shield)
    {
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

    public void RemoveStatusEffect(StatusEffect effect) { 
        appliedStatusEffects.Remove(effect);
    }

    public void DestroySelf()
    {
        occupiedCell.occupiedEntity = null;
        Destroy(this.gameObject);

        OnDeath?.Invoke();
    }
}