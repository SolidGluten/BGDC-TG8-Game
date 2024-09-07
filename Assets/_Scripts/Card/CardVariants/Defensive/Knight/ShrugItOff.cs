using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShrugItOff", menuName = "ScriptableObjects/Cards/Shrug It Off")]

public class ShrugItOff : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        from.TakeDamage(from.stats.HP * dmgMultiplier / 100);
        from.GainShield(from.stats.ATK * gainShieldMultiplier / 100);
        CardManager.instance.DrawCard();
        return true;
    }

}

