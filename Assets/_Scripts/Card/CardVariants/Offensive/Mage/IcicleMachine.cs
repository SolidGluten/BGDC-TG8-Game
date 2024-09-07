using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IcicleMachine", menuName = "ScriptableObjects/Cards/Mage/Offensive/Icicle Machine")]
public class IcicleMachine : Card
{
    public override bool Play(Entity from, Entity[] target, int dmgMultiplier = 0, int healMultiplier = 0, int gainShieldMultiplier = 0)
    {
        return false;
    }
}
