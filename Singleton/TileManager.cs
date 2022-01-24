using Assets.Scripts;

using Assets.Scripts.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public int MapSize = 20;

    [Header("Tilemaps")]
    public Tilemap GroundMap;
    public Tilemap BuildingMap;
    public Tilemap OverlayMap;

    [HideInInspector]
    public TileDataSO[] AllGroundTileData;
    int[,] GroundMatrix;

    [HideInInspector]
    public TileDataSO[] AllBuildingTileData;
    int[,] BuildingMatrix;


    TerrainGenerator TerrainGenerator;
    public void Awake()
    {
        TerrainGenerator = FindObjectOfType<TerrainGenerator>();
    }
    public void Start()
    {
        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        AllBuildingTileData = Resources.LoadAll("Buildings")
            .Select(o => (TileDataSO)o)
            .ToArray();
        BuildingMatrix = TerrainGenerator.GenerateTerrain_Height(AllBuildingTileData, BuildingMap, MapSize);

        AllGroundTileData = Resources.LoadAll("Ground")
            .Select(o => (TileDataSO)o)
            .ToArray();
        GroundMatrix = TerrainGenerator.GenerateTerrain_Height(AllGroundTileData, GroundMap, MapSize);
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
        Debug.Log($"Ground: {GroundMatrix[cell.x, cell.y]} - Building: {BuildingMatrix[cell.x, cell.y]}");
        Debug.Log($"Ground: {GroundMap.HasTile(cell)} - Building: {BuildingMap.HasTile(cell)}");
        
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


}
