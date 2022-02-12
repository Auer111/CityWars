using Assets.Data;
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


        [HideInInspector]
        public int Id;

        public string Text;
        public Sprite Sprite;
        public TileBase Tile;

        [Header("Block Future generation on this layer.")]
        public bool BlockGeneration;

        public TileMaps TargetGenerationLayer = TileMaps.Any;

        [Header("-1 Does not generate on startup.")]
        [Range(-1,100)]
        public int Height = -1;

        public List<TileDataSO> Upgrades;

        public void Init()
        {
            Id = Tile != null
                ? GenerateID
                : BlockGeneration
                    ? -1
                    : 0;

            //Debug.Log("INIT:\n"+"Name:" + name + "    Id:" + Id + "    Block:" + BlockGeneration);
        }

    }
}
