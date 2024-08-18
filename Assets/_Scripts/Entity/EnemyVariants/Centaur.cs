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

        var endOfLine = attackArea.Last();
        endOfLine.SetEntity(from);

        return true;
    }
}
