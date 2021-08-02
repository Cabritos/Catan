using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [SerializeField] private BoardLayout _boardLayout = null;
    [SerializeField] private Tilemap _tilemap = null;

    private Dictionary<TileType, int> _maxValues = new Dictionary<TileType, int>();
    private Dictionary<TileType, int> _typeCount = new Dictionary<TileType, int>();

    private TilesDictionary _tilesDictionary;
    private GridLayout _gridLayout;

    void Awake()
    {
        _tilesDictionary = GetComponent<TilesDictionary>();
        _gridLayout = _tilemap.layoutGrid;
    }

    void Start()
    {
        SetMaxResourcesTilesValues();
        _tilemap.ClearAllTiles();
        _tilesDictionary.GenerateDictionary();
        SetTilemap();
    }

    private void SetTilemap()
    {
        BoardTile[] boardTiles = GetComponentsInChildren<BoardTile>();
        int count = 0;

        foreach (var boardTile in boardTiles)
        {
            boardTile.SetGridPosition(count, _gridLayout.WorldToCell(boardTile.transform.position));
            boardTile.SetWorldPosition(_gridLayout.CellToWorld(boardTile.GridPosition));
            count++;

            if (boardTile.IsLand)
            {
                AddTile(boardTile, SetLandTile());
                continue;
            }

            boardTile.SetHarbor();

            if (boardTile.IsResourceHarbor)
            {
                AddTile(boardTile, SetHarborTile());
                continue;
            }

            if (boardTile.IsHarbor)
            {
                AddTile(boardTile, TileType.Harbor);
                continue;
            }

            AddTile(boardTile, TileType.Water);
        }
    }

    private void SetMaxResourcesTilesValues()
    {
        _maxValues.Add(TileType.Desert, _boardLayout.GetValues().desert);
        _maxValues.Add(TileType.Brick, _boardLayout.GetValues().brick);
        _maxValues.Add(TileType.Grain, _boardLayout.GetValues().grain);
        _maxValues.Add(TileType.Ore, _boardLayout.GetValues().ore);
        _maxValues.Add(TileType.Wood, _boardLayout.GetValues().wood);
        _maxValues.Add(TileType.Wool, _boardLayout.GetValues().wool);
    }

    private void AddTile(BoardTile boardTile, TileType tileType)
    {
        boardTile.SetTileType(tileType);
        var baseTile = _tilesDictionary.GetTileBase(boardTile.TileType);
        _tilemap.SetTile(boardTile.GridPosition, baseTile);
    }

    private TileType SetLandTile()
    {
        TileType tileType = TileType.Water;
        var tryingToSet = true;

        var count = 0;

        while (tryingToSet)
        {
            tileType = (TileType) UnityEngine.Random.Range(0, 6);

            if (!_typeCount.ContainsKey(tileType))
            {
                _typeCount.Add(tileType, 1);
                break;
            }

            if (_typeCount[tileType] >= _maxValues[tileType])
            {
                count++;
                if (count == 100)
                {
                    tryingToSet = false;
                    Debug.LogError("Error in land tiling ");
                }
                continue;
            }

            _typeCount[tileType]++;
            tryingToSet = false;
        }

        return tileType;
    }

    private TileType SetHarborTile()
    {
        TileType tileType = TileType.Water;

        int count = 0;
        while (true)
        {
            tileType = (TileType)UnityEngine.Random.Range(7, 13);
            Debug.Log(tileType);

            if (!_typeCount.ContainsKey(tileType))
            {
                _typeCount.Add(tileType, 1);
                Debug.Log("Added");
                break;
            }

            Debug.Log("Retry");
            count++;
            if (count == 100)
            {
                Debug.LogError("Error in harbor tiling ");
                foreach (var i in _typeCount)
                {
                    Debug.Log(i);
                }

                break;
            }
        }

        return tileType;
    }
}
