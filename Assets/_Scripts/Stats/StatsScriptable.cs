using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "ScriptableObjects/Stats")]
public class StatsScriptable : ScriptableObject
{
    public int HP;
    public int ATK;
    public int MOV;
}
