using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
