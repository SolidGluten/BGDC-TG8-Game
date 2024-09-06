using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cure", menuName = "ScriptableObjects/Cards/Mage/Support/Cure")]
public class Cure : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        return base.Play(from, target, dmgMultiplier, healMultiplier, gainShieldMultiplier);
    }
}
