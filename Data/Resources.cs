using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(menuName = "Resources")]
    public class ResourceLoader : ScriptableObject
    {
        public List<Assets> AllTiles;


        public static List<TileDataSO> Load()
        {
            ResourceLoader Loader = (ResourceLoader)Resources.Load("Loaders", typeof(ResourceLoader));
            var all = Loader.AllTiles.SelectMany(i => i.TilesForMap).ToList();

            foreach(Assets tileList in Loader.AllTiles){
                tileList.TilesForMap.ForEach(i => i.TargetGenerationLayer = tileList.Map);
            }
            all.ForEach(so => { so.Init(); });
            return all;
        }
    }

    [Serializable]
    public class Assets
    {
        public TileMaps Map;
        public List<TileDataSO> TilesForMap;
    }
}
