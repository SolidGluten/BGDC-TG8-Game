using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NormalGoblin", menuName = "ScriptableObjects/Enemies/NormalGoblin")]
public class NormalGoblin : EnemyScriptable
{
    public override bool Attack(Enemy from, Character[] targets)
    {
        if (!targets.Any()) return false;

        foreach(Character chara in targets)
            chara?.TakeDamage(from.stats.ATK);

        return true;
    }
}
