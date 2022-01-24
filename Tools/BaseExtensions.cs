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
    }
}
