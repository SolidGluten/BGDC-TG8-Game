using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class EditorCustomUtils : MonoBehaviour
{
#if UNITY_EDITOR
    public static void DestroyOnEdit(GameObject obj)
    {
        EditorApplication.delayCall += () =>
        {
            DestroyImmediate(obj);
        };
    }
#endif
}
