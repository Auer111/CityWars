using Assets.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Assets.Scripts.Tools;
using Unity.VectorGraphics;

namespace Assets.Scripts
{
    public class TileDataRenderer : MonoBehaviour, IScriptableObjectRenderer<TileDataSO>
    {
        public GameObject Image;
        public void Init(TileDataSO data)
        {
            gameObject.name = data.name;
            Image img = Image.GetComponent<Image>();
            img.sprite = data.Sprite;

            GetComponentInChildren<Button>().onClick.AddListener(delegate {
                FindObjectOfType<TileSelect>().SetTile(data); 
            });
        }

    }
}
