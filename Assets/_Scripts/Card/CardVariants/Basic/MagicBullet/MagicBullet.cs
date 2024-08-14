using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicBullet", menuName = "ScriptableObjects/Cards/MagicBullet")]
public class MagicBullet : Card
{
    public override bool Play(Entity from, Entity[] target)
    {
        return true;
    }
}
