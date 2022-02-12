using Assets.Scripts;
using Assets.Scripts.Data;
using Assets.Scripts.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public int MapSize = 20;

    public TileCategorySO[] Categories;

    [Header("Tilemaps")]
    public Tilemap GroundMap;
    public Tilemap BuildingMap;
    public Tilemap OverlayMap;

    [HideInInspector]
    public TileDataSO[] AllGroundTileData;
    [HideInInspector]
    public int[,] GroundMatrix;

    [HideInInspector]
    public TileDataSO[] AllBuildingTileData;
    [HideInInspector]
    public int[,] BuildingMatrix;

    public void Start()
    {
        GroundMatrix = new int[MapSize, MapSize];
        BuildingMatrix = new int[MapSize, MapSize];

        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        List<TileDataSO> Data = Resources.LoadAll("Tiles").Select(o => (TileDataSO)o).Init().ToList();

        AllBuildingTileData = Data
            .Where(td => td.TargetGenerationLayer == TileMaps.Building).ToArray();
        //BuildingMatrix = TerrainGenerator.GenerateTerrain_Height(AllBuildingTileData, BuildingMap, MapSize);

        AllGroundTileData = Data
            .Where(td => td.TargetGenerationLayer == TileMaps.Ground).ToArray();
        //GroundMatrix = TerrainGenerator.GenerateTerrain_Height(AllGroundTileData, GroundMap, MapSize);


        GetComponent<TerrainGenerator>().GenerateAll(this);
    }

    public List<TileDataSO> GetAvailableBuildings(Vector3Int cell)
    {
        var TargetTile = GetTopTileData(cell);
        if (!TargetTile) { return null; }

        return TargetTile.Upgrades;
    }
    public List<TileDataSO> GetAvailableBuildings(TileDataSO data)
    {
        if (!data) { return null; }

        return data.Upgrades;
    }

    public void UpdateTile(Vector3Int cell, TileDataSO data)
    {
        BuildingMap.SetTile(cell, data.Tile);
        //Debug.Log($"Ground: {GroundMatrix[cell.x, cell.y]} - Building: {BuildingMatrix[cell.x, cell.y]}");
        //Debug.Log($"Ground: {GroundMap.HasTile(cell)} - Building: {BuildingMap.HasTile(cell)}");
        BuildingMatrix[cell.x, cell.y] = data.Id;
    }

    public TileDataSO GetTopTileData(Vector3Int cell)
    {
        if(BuildingMatrix[cell.x, cell.y] > 0){
            return AllBuildingTileData.FirstOrDefault(b => b.Id == BuildingMatrix[cell.x, cell.y]);
        }
        else if(GroundMatrix[cell.x, cell.y] > 0){
            return AllGroundTileData.FirstOrDefault(b => b.Id == GroundMatrix[cell.x, cell.y]);
        }
        return null;
    }


    public Tilemap GetMap(TileMaps map)
    {
        return map switch
        {
            TileMaps.Ground => GroundMap,
            TileMaps.Building => BuildingMap,
            TileMaps.Overlay => null,
            _ => null,
        };
    }
    public int[,] GetMatrix(TileMaps matrix){
        return matrix switch{
            TileMaps.Ground => GroundMatrix,
            TileMaps.Building => BuildingMatrix,
            TileMaps.Overlay => null,
            _ => null,
        };
    }
}
