using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridSystem))]
[CanEditMultipleObjects]
public class GridSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GridSystem gridSystem = (GridSystem)target;
        if (GUILayout.Button("Regenerate Grid"))
            gridSystem.RegenerateGrid();
    }
}
