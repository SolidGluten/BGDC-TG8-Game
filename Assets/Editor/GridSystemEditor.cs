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

        GUILayout.Space(10);

        if (GUILayout.Button("Save Grid Data"))
            gridSystem.SaveGridData();
        if (GUILayout.Button("Load Grid Data"))
            gridSystem.LoadGridData();

        GUILayout.Space(10);

        if (GUILayout.Button("Regenerate Grid"))
            gridSystem.RegenerateGrid();
        if (GUILayout.Button("Show Cost"))
            gridSystem.ShowCost(!gridSystem.isCostShown);
    }
}
