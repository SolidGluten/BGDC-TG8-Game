using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "ScriptableObjects/Stats")]
public class StatsScriptable : ScriptableObject
{
    [SerializeField] public int HP;
    [SerializeField] public int ATK;
    [SerializeField] public int MOV;
}
