using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Resources/Tiles")]
    public class TileDataSO : ScriptableObject
    {
        //Create Unique Ids
        static int GenerateID { get { IdGenerator++; return IdGenerator; } }
        static int IdGenerator = 0;

        [Header("Leave empty at height range.")]
        public bool isNull = false;
        [HideInInspector]
        public int Id = GenerateID;
        
        public string Text;
        public Sprite Sprite;
        public TileBase Tile;

        [Header("-1 Does not generate on startup.")]
        [Range(-1,100)]
        public int Height = -1;

        public List<TileDataSO> Upgrades;
    }
}
