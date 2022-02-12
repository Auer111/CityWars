using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Tools
{
    public static class TileDataExtensions
    {

        public static TileDataSO From(this TileDataSO[] tiles, int id)
        {
            return tiles.ToList().FirstOrDefault(t => t.Id == id);
        }

        public static TileDataSO From(this TileDataSO[] tiles, TileBase tile)
        {
            return tiles.ToList().FirstOrDefault(t => t.Tile == tile);
        }


        public static void FloodFill(this Tilemap map, int MapSize, Vector3Int cell, TileBase tile, int radius)
        {
            int xMin = Mathf.Max(0, cell.x - radius);
            int xMax = Mathf.Min(MapSize - 1, cell.x + radius + 1);

            int yMin = Mathf.Max(0, cell.y - radius);
            int yMax = Mathf.Min(MapSize - 1, cell.y + radius + 1);

            Debug.Log($"{xMin} {xMax} {yMin} {yMax}");

            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    map.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }

    }
}
