using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Spider", menuName = "ScriptableObjects/Enemies/Spider")]
public class Spider : EnemyScriptable
{
    public override void Attack(Enemy from, Character[] targets, List<Cell> attackArea = null)
    {
        if (!targets.Any()) return;

        foreach (Character chara in targets)
            chara?.TakeDamage(from.stats.ATK);
    }
}
