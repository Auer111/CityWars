using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.Tools;
using System.Linq;
using Assets.Scripts;
using UnityEngine.UI;
using TMPro;

public class TileSelect : MonoBehaviour
{
    TileManager TM;

    bool MenuOpen = false;
    Vector3Int SelectedCell;

    [Header("UI")]
    public GameObject TileInfoPanel;
    public GameObject TileSelectionPrefab;
    public GameObject TileSelectionContainer;

    [Header("HoverTile")]
    public TileBase GroundTileHovered;
    Vector3Int HoveredCell;
    TileBase GroundTileDefault;

    private void Awake()
    {
        TM = FindObjectOfType<TileManager>();
    }

    private void Start()
    {
        ShowMenu(false);
    }

    void Update()
    {
        //if (Functions.IsPointerOverUIElement()) { return; }

        bool clicked = false;// Input.GetMouseButtonDown(0);

        Vector3 MousePos = new Vector3Int().Null();// Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = 0f;
        Vector3Int cell = TM.BuildingMap.WorldToCell(MousePos);

        if (cell.x < 0 || cell.y < 0) { return; }

        if (clicked){
            Debug.Log(cell.x + "-" + cell.y);

            SelectedCell = cell;
            Hover(cell);
            LoadMenu(cell);
            return;
        }

    }

    void ShowMenu(bool state)
    {
        if(state == false){
            ClearSelection();
        }
        MenuOpen = state;
        TileSelectionContainer.transform.parent.parent.parent.gameObject.SetActive(state);
    }
    void LoadMenu(Vector3Int cell)
    {
        var TileData = TM.GetTopTileData(cell);
        if (!TileData) {
            Debug.Log($"Tile Data Resource Missing");
            ClearSelection();
            return; 
        }

        ShowMenu(true);

        TextMeshProUGUI[] TMPs = TileInfoPanel.GetComponentsInChildren<TextMeshProUGUI>();
        TMPs[0].text = TileData.name;
        TMPs[1].text = TileData.Text;
        Image img = TileInfoPanel.GetComponentsInChildren<Image>().Last();
        img.sprite = TileData.Sprite;
        //img.CenterSpriteOnPivotY();

        foreach (Transform child in TileSelectionContainer.transform) { Destroy(child.gameObject); }
        if (TileData.Upgrades.Any()){
            TileSelectionContainer.transform.parent.parent.gameObject.SetActive(true);
            TileData.Upgrades.ForEach(b => Instantiate(TileSelectionPrefab, TileSelectionContainer.transform).LoadScriptableObject(b));
        }
        else{
            TileSelectionContainer.transform.parent.parent.gameObject.SetActive(false);
        }
        
    }

    void ClearSelection()
    {
        HoveredCell = new Vector3Int().Null();
        SelectedCell = new Vector3Int().Null();
        TM.OverlayMap.ClearAllTiles();
    }

    void Hover(Vector3Int currentCell)
    {
        if (currentCell != HoveredCell)
        {
            TM.OverlayMap.ClearAllTiles();
            TM.OverlayMap.SetTile(currentCell, GroundTileHovered);
            HoveredCell = currentCell;
        }
    }

    public void SetTile(TileDataSO tile){
        if (SelectedCell.isNull()) { return; }
        TM.UpdateTile(SelectedCell,tile);
        TM.OverlayMap.ClearAllTiles();
        ShowMenu(false);
    }
}
