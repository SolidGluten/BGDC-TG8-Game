using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Centaur", menuName = "ScriptableObjects/Enemies/Centaur")]
public class Centaur : EnemyScriptable
{
    private new void OnValidate()
    {
        maxRangeFromTarget = Mathf.Max(maxRangeFromTarget, 1);
        attackWidth = 0;
        attackShape = HighlightShape.Line;
    }

    public override bool Attack(Enemy from, List<Cell> attackArea)
    {
        var characters = attackArea?
            .Where((cell) => cell && cell.isOccupied)?
            .Select((cell) => cell.occupiedEntity.GetComponent<Character>())?
            .Where((chara) => chara != null);

        if (!characters.Any()) return false;

        foreach (var chara in characters)
            chara.TakeDamage(from.stats.ATK);

        var cell = attackArea.First();

        if (cell.isOccupied)
        {
            return true;
        }

        for (int i = 0; i < attackArea.Count; i++)
        {
            if (attackArea[i].isOccupied && i != 0)
            {
                cell.SetEntity(from);
                break;
            }
            cell = attackArea[i];
        }

        return true;
    }
}
