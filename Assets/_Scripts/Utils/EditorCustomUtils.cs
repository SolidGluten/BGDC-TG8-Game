using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCustomUtils : MonoBehaviour
{
    public static void DestroyOnEdit(GameObject obj)
    {
        UnityEditor.EditorApplication.delayCall += () =>
        {
            DestroyImmediate(obj);
        };
    }
}
