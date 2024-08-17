using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScriptable : ScriptableObject
{
    public int detectionRange = 3;
    public int maxRangeFromTarget = 0;
    public int attackRange = 2;
    public HighlightShape attackShape;

    public abstract void Attack(Enemy from, Character[] targets);
}
