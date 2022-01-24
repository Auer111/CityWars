using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(TerrainGenerator))]
class TerrainGeneratorCE : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Generate") && Application.isPlaying)
        {
            FindObjectOfType<TileManager>().GenerateTerrain();
        }

    }
}

