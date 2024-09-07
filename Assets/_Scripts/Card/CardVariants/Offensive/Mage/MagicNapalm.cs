using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicNapalm", menuName = "ScriptableObjects/Cards/Mage/Offensive/Magic Napalm")]
public class MagicNapalm : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        var enemies = GetAllTargetEnemies(target);
        if (!enemies.Any()) return false;

        foreach (var enemy in enemies)
        {
            enemy.TakeDamage(from.stats.ATK * dmgMultiplier / 100);
        }

        for(int i = 0; i < 2; i++)
        {
            var mageCard = CardManager.instance.DrawCharacterCard(CharacterType.Mage);
            if (mageCard != null) mageCard.ReduceCost(1); 
        }

        return true;
    }
}
