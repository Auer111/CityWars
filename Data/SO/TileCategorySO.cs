using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "Resources/Tiles")]
    public class TileCategorySO : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public string Description;

        public TileDataSO[] Tiles;
    }
}
