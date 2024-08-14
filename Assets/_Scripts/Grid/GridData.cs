using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridData
{
    public GridData(int _width, int _height, Vector2Int[] _characterSpawnPos, Vector2Int[] _enemySpawnPos)
    {
        width = _width;
        height = _height;
        characterSpawnPos = _characterSpawnPos;
        enemySpawnPos = _enemySpawnPos;
    }

    public int width;
    public int height;

    public Vector2Int[] characterSpawnPos;
    public Vector2Int[] enemySpawnPos;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
