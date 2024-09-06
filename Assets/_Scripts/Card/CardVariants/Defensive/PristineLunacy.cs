using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PristineLunacy", menuName = "ScriptableObjects/Cards/Knight/Defensive/Pristine Lunacy")]

public class PristineLunacy : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var enemies = GetAllTargetEnemies(target);
        foreach (Enemy ene in enemies)
        {
            from.GainHealth(from.stats.HP * healMultiplier / 100);
        }
        return true;
    }
}

