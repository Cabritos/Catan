using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapTools : MonoBehaviour
{
    [SerializeField] Dictionary<int, BoardTile> _boardTiles = new Dictionary<int, BoardTile>();

    [SerializeField] private Tilemap _waterTileMap = null;
    [SerializeField] private Tilemap _landTileMap = null;
    [SerializeField] private GameObject _boardTilePrefab = null;

    private GridLayout _gridLayout;

    void Start()
    {
        _gridLayout = _waterTileMap.GetComponentInParent<GridLayout>();

        //TileBase[] allTiles = _waterTileMap.GetTilesBlock(_waterTileMap.cellBounds);

        //SetTiles(_waterTileMap);
    }

    
    private void SetTiles(Tilemap tileMap)
    {
        foreach (Vector3Int pos in _waterTileMap.cellBounds.allPositionsWithin)
        {
            if (!tileMap.HasTile(pos)) continue;

            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            var tile = Instantiate(_boardTilePrefab);
            //tile.GetComponent<BoardTile>().SetPosition(localPlace, _gridLayout.CellToWorld(localPlace));
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = _waterTileMap.WorldToCell(mousePos);

            var tile = _waterTileMap.GetTile(gridPos);

            Debug.Log(gridPos);
        }
    }
}
