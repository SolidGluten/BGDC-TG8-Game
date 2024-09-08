using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Empowered", menuName = "ScriptableObjects/StatusEffect/Empowered")] 
public class Empowered : Effect
{
    public int empoweredPercentMultip = 25;

    public override void ApplyEffect(Entity target, int effectMultip = 0)
    {
        var empoweredDamage = target.currAttackDamage * empoweredPercentMultip / 100;
        target.currAttackDamage += empoweredDamage;
    }

    public override void RemoveEffect(Entity target)
    {
        var empoweredDamage = target.currAttackDamage * empoweredPercentMultip / 100;
        target.currAttackDamage -= empoweredDamage;
    }
}
