using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public static class BaseExtensions
    {
        public static Vector3Int Null(this Vector3Int v3)
        {
            return new Vector3Int(-100, -100, 0);
        }

        public static bool isNull(this Vector3Int v3)
        {
            return v3 == new Vector3Int(-100, -100, 0);
        }

        public static List<TileDataSO> Init(this IEnumerable<TileDataSO> SOS)
        {
            var L = SOS.ToList();
            L.ForEach(so => { so.Init(); });
            return L;
        }

        public static Vector3 V3(this Vector2 v2, float z = 0) 
        {
            return new Vector3(v2.x, v2.y, z);
        }
        public static Vector2 V2(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }

    }
}
