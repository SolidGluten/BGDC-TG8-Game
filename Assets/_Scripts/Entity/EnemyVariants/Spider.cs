using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Spider", menuName = "ScriptableObjects/Enemies/Spider")]
public class Spider : EnemyScriptable
{
    public override bool Attack(Enemy from, List<Cell> attackArea)
    {
        var characters = attackArea
            .Where((cell) => cell && cell.isOccupied)
            .Select((cell) => cell.occupiedEntity.GetComponent<Character>())
            .Where((chara) => chara != null);

        if (!characters.Any()) return false;

        foreach (var chara in characters)
        {
            chara.TakeDamage(from.stats.ATK);
        }

        return true;
    }
}
