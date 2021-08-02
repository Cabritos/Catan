using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapStuff : MonoBehaviour
{
    [SerializeField] Dictionary<int, BoardTile> _boardTiles = new Dictionary<int, BoardTile>();

    [SerializeField] private Tilemap _waterTileMap = null;
    [SerializeField] private Tilemap _landTileMap = null;
    [SerializeField] private GameObject _boardTilePrefab;

    private GridLayout _gridLayout;

    void Start()
    {
        _gridLayout = _waterTileMap.GetComponentInParent<GridLayout>();

        //TileBase[] allTiles = _waterTileMap.GetTilesBlock(_waterTileMap.cellBounds);


    }

    /*
    private void SetTiles(bool isLand, Tilemap tileMap)
    {
        foreach (Vector3Int pos in _waterTileMap.cellBounds.allPositionsWithin)
        {
            if (!tileMap.HasTile(pos)) continue;

            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Debug.Log(localPlace);

            var boardTileGameObject = Instantiate(_boardTilePrefab);
            var boardTile = boardTileGameObject.GetComponent<BoardTile>();

            boardTile.SetValues(
                tileMap.GetTile(localPlace),
                localPlace,
                _gridLayout.CellToWorld(localPlace),
                _boardTiles.Count,
                tileMap.GetTile(pos),
                isLand);

            _boardTiles.Add(_boardTiles.Count, boardTile);
        }
    }
    */

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
