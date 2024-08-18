using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Centaur", menuName = "ScriptableObjects/Enemies/Centaur")]
public class Centaur : EnemyScriptable
{
    public override void Attack(Enemy from, Character[] targets, List<Cell> attackArea = null)
    {
        if (!targets.Any()) return;

        foreach (Character chara in targets)
            chara?.TakeDamage(from.stats.ATK);

        var endOfLine = attackArea.Aggregate(attackArea.First(), (acc, x) => Mathf.Abs(x.index.x) > Mathf.Abs(acc.index.x) ? x : acc);
        endOfLine.SetEntity(from);
    }
}
