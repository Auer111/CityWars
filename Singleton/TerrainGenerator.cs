using Assets.Scripts;
using Assets.Scripts.Data;
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
    public List<Layer> Layers;

    public void GenerateAll(TileManager TM)
    {
        foreach (Layer L in Layers)
        {
            foreach (Pass p in L.GenerationPasses)
            {
                GenerateTerrain_Height(p.Tile.ToArray(), TM.GetMap(L.Map), TM.GetMatrix(L.Map), TM.MapSize, p.noiseScale, p.offset);
            }
        }
    }

    int[,] GenerateTerrain_Height(TileDataSO[] data, Tilemap map, int[,]Matrix, int MapSize, float noiseScale, int offset)
    {
        int[,] noiseMap = Noise.GenerateNoiseMap(MapSize, MapSize, noiseScale, offset);

        List<TileDataSO> Ordered = data.ToList()
            .OrderBy(t => t.Height)
            .ToList();
        
        for (int x = 0; x < MapSize; x++){
            for (int y = 0; y < MapSize; y++){
                var cell = new Vector3Int(x, y, 0);

                if (Matrix[x,y] != 0) { continue; }
                TileDataSO tileData = Ordered.FirstOrDefault(t => t.Height >= noiseMap[x, y]) ?? Ordered.Last();
                map.SetTile(cell, tileData.Tile);
                Matrix[x, y] = tileData.Id;
            }
        }
        return Matrix;
    }


    [Serializable]
    public class Pass
    {
        public float noiseScale;
        public int offset;
        public List<TileDataSO> Tile;
    }

    [Serializable]
    public class Layer
    {
        public TileMaps Map;
        public List<Pass> GenerationPasses;
    }
}

