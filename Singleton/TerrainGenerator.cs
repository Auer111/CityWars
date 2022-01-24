using Assets.Scripts;
using Assets.Scripts.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour
{
    public float noiseScale;

    public int[,] GenerateTerrain_Height(TileDataSO[] data, Tilemap map, int MapSize)
    {
        int[,] noiseMap = Noise.GenerateNoiseMap(MapSize, MapSize, noiseScale);
        int[,] Matrix = new int[MapSize, MapSize];

        List<TileDataSO> Ordered = data.ToList()
            .OrderBy(t => t.Height)
            .ToList();

        for (int y = 0; y < MapSize; y++){
            for (int x = 0; x < MapSize; x++){
                var cell = new Vector3Int(x, y, 0);
                if (map.HasTile(cell)) {
                    Matrix[cell.x, cell.y] = data.From(map.GetTile(cell))
                        ?.Id 
                        ?? 0;
                    continue; 
                }
                TileDataSO tileData = Ordered.FirstOrDefault(t => t.Height >= noiseMap[x, y]) ?? Ordered.Last();
                map.SetTile(cell, tileData.Tile);
                Matrix[cell.x, cell.y] = tileData.Id;
            }
        }
        return Matrix;
    }
}

