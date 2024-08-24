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

    public List<AppliedStatusEffect> appliedStatusEffects = new List<AppliedStatusEffect>();

    public int currHealth;
    public int currShield = 0;
    public int currMovePoints;
    public int currAttackDamage;

    public bool canMove = true;

    public bool canPlay;

    public event Action OnDeath;

    public void Awake()
    {
        TurnController.instance.OnStartTurn += UpdateStatusEffect_OnStart;
        TurnController.instance.OnEndTurn += UpdateStatusEffect_OnEnd;
    }

    public void Start()
    {
        currHealth = stats.HP;
        currMovePoints = stats.MOV;
        currAttackDamage = stats.ATK;
    }

    public void TakeDamage(int damage)
    {
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

        if (currHealth <= 0) DestroySelf();
    }
    public void GainShield(int shield)
    {
        //Reminder to add Sturdy
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

    public void ApplyStatusEffect(Entity from, StatusEffect statusEffect)
    {
        var appEffect = appliedStatusEffects.Find((x) => x.statusEffect.effect == statusEffect.effect);
        if (appEffect != null)
        {
            if (statusEffect.effect.isStackable) return;
            appEffect.statusEffect.stacks += 1;
        }
        else
        {
            appliedStatusEffects.Add(new AppliedStatusEffect(statusEffect.Clone(), from, this));
        }
    }

    public void UpdateStatusEffect_OnStart()
    {
        List<AppliedStatusEffect> appEffects = appliedStatusEffects.Where(x => x.statusEffect.effect.isAppliedOnStart).ToList();
        for (int i = 0; i < appEffects.Count; i++)
        {
            // Apply effect again!
            appEffects[i].UseEffect();
            //Debug.Log(appEffects[i].statusEffect.effect.name + "Effect applied");

            if (appEffects[i].statusEffect.effect.isPermanent) return;

            appEffects[i].statusEffect.stacks -= 1;
            if (appEffects[i].statusEffect.stacks == 0)
            {
                // Remove effect
                appliedStatusEffects[i].RemoveEffect();
                appliedStatusEffects.Remove(appEffects[i]);
            }
        }
    }

    public void UpdateStatusEffect_OnEnd()
    {
        List<AppliedStatusEffect> appEffects = appliedStatusEffects.Where(x => !x.statusEffect.effect.isAppliedOnStart).ToList();
        for (int i = 0; i < appEffects.Count; i++)
        {
            // Apply effect again!
            appEffects[i].UseEffect();
            //Debug.Log(appEffects[i].statusEffect.effect.name + "Effect applied");

            if (appEffects[i].statusEffect.effect.isPermanent) return;

            appEffects[i].statusEffect.stacks -= 1;
            if (appEffects[i].statusEffect.stacks == 0)
            {
                // Remove effect
                appliedStatusEffects[i].RemoveEffect();
                appliedStatusEffects.Remove(appEffects[i]);
            }
        }
    }

    public void DestroySelf()
    {
        occupiedCell.occupiedEntity = null;
        Destroy(this.gameObject);

        TurnController.instance.OnStartTurn -= UpdateStatusEffect_OnStart;
        TurnController.instance.OnEndTurn -= UpdateStatusEffect_OnEnd;

        OnDeath?.Invoke();
    }
}

[Serializable]
public class AppliedStatusEffect
{
    public AppliedStatusEffect(StatusEffect effect, Entity caster, Entity target)
    {
        this.statusEffect = effect;
        this.caster = caster;
        this.target = target;
    }

    [SerializeField] private Entity caster;
    [SerializeField] private Entity target;
    public StatusEffect statusEffect;

    public void UseEffect()
    {
        statusEffect.effect.ApplyEffect(caster, target);
    }

    public void RemoveEffect()
    {
        statusEffect.effect.RemoveEffect(caster, target);
    }
}