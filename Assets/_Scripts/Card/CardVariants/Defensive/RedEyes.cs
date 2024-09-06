using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RedEyes", menuName = "ScriptableObjects/Cards/Knight/Defensive/RedEyes")]

public class RedEyes : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var enemies = GetAllTargetEnemies(target);
        foreach (Enemy ene in enemies)
        {
            CardManager.instance.DrawCard();
        }
        return true;
    }
}
