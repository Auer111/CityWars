using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public static class Noise
    {
        public static int[,] GenerateNoiseMap(int mapWidth,int mapHeight, float scale)
        {
            int[,] noiseMap = new int[mapWidth, mapHeight];

            if(scale <= 0){
                scale = 0.0001f;
            }

            for(int y=0; y<mapHeight; y++)
            {
                for(int x=0;x<mapWidth; x++)
                {
                    float sampleX = x / scale;
                    float sampleY = y / scale;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                    noiseMap[x, y] =  Mathf.RoundToInt(perlinValue * 100);
                }
            }
            return noiseMap;
        }
    }
}
