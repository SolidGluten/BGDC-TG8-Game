using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SunsGrace", menuName = "ScriptableObjects/Cards/SunsGrace")]
public class SunsGrace : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
