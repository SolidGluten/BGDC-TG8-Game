using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapMaker : MonoBehaviour
{
    public GridSystem grid;
    public GridData gridData;
    public TextAsset jsonGridData;


    public void ReadFromGridData()
    {
        grid.gridPos = gridData.gridPos;
        grid.width = gridData.width;
        grid.height = gridData.height;
        grid.characterSpawnPositions = gridData.characterSpawnPos;
        grid.enemySpawnPositions = gridData.enemySpawnPos;
    }

    [ContextMenu("Save Grid Data")]
    public void SaveGridData()
    {
        GridData data = new GridData(grid.width, grid.height, grid.characterSpawnPositions.ToArray(), grid.enemySpawnPositions.ToArray(), grid.gridPos);

        string GData = JsonUtility.ToJson(data);
        Debug.Log(GData);
        File.WriteAllText(Application.dataPath + "/Resources/GridData/data.json", GData);
    }

    [ContextMenu("Load Grid Data")]
    public void LoadGridData()
    {
        if (!jsonGridData)
        {
            Debug.LogWarning("No data to load from.");
            return;
        }
        GridData loadedData = JsonUtility.FromJson<GridData>(jsonGridData.text);
        gridData = loadedData;
    }

    [ContextMenu("Get Random GridData")]
    public void GetRandomGridData()
    {
        var jsonDatas = Resources.LoadAll("GridData", typeof(TextAsset)).ToList();
        if (jsonDatas == null || !jsonDatas.Any())
        {
            Debug.LogWarning("No Grid Data found!");
            return;
        }
        var randomIdx = UnityEngine.Random.Range(0, jsonDatas.Count);
        jsonGridData = jsonDatas[randomIdx] as TextAsset;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MapMaker))]
public class MapMakerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MapMaker mapMaker = (MapMaker)target;

        GUILayout.Space(15);

        if (GUILayout.Button("Read Grid Data"))
        {
            mapMaker.ReadFromGridData();
            EditorUtility.SetDirty(mapMaker);
            EditorUtility.SetDirty(mapMaker.grid);
        }

        if(GUILayout.Button("Save Grid Data"))
        {
            mapMaker.SaveGridData();
        }

        if(GUILayout.Button("Load Grid Data"))
        {
            mapMaker.LoadGridData();
            EditorUtility.SetDirty(mapMaker);
        }

        if(GUILayout.Button("Get Random Grid"))
        {
            mapMaker.GetRandomGridData();
            EditorUtility.SetDirty(mapMaker);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
