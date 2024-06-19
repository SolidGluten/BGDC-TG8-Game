using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private GridSystem grid;
    [SerializeField] private GameObject wallObj;
    [SerializeField]
    [Range(0, 20)] private int wallCount = 0;
    [SerializeField] private List<Wall> walls = new List<Wall>();


    [ContextMenu("Initialize Walls")]
    public void InitializeWalls()
    {
        walls.ForEach(wall => wall.DestroySelf());
        walls.Clear();
        for(int i = 0; i < wallCount; i++)
        {
            walls.Add(AddWall());
        }
    }

    private Vector2Int GetRandomSpawnPos()
    {
        bool validPos;
        Vector2Int pos;
        do
        {
            validPos = true;
            pos = new Vector2Int(Random.Range(0, grid.Width), Random.Range(0, grid.Height));
            if (grid.ValidateCell(pos)) validPos = false;
        } while (!validPos);

        return pos;
    }

    private Wall AddWall()
    {
        var spawnPos = GetRandomSpawnPos();
        var wall = Instantiate(wallObj).GetComponent<Wall>();
        var cell = grid.GetCell(spawnPos);
        cell.SetObject(wall.gameObject);
        wall.CurrCell = cell;
        return wall;
    }
    
}
