using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [SerializeField] private BoardLayout _boardLayout = null;
    [SerializeField] private Tilemap _landTilemap = null;
    [SerializeField] private BoardTile[] _boardTiles = null;
    
    private Dictionary<TileType, int> _maxValues = new Dictionary<TileType, int>();
    private Dictionary<TileType, int> _typeCount = new Dictionary<TileType, int>();

    private TilesDictionary _tilesDictionary;

    void Awake()
    {
        _tilesDictionary = GetComponent<TilesDictionary>();
    }

    void Start()
    {
        SetMaxResourcesTilesValues();
        _landTilemap.ClearAllTiles();
        SetTilemap();
    }

    private void SetTilemap()
    {
        foreach (var boardTile in _boardTiles)
        {
            if (!boardTile.IsLand) continue;

            boardTile.SetTileType(AddLandTile());

            var baseTile = _tilesDictionary.GetTileBase(boardTile.TileType);
            _landTilemap.SetTile(boardTile.Position, baseTile);
        }
    }

    private void SetMaxResourcesTilesValues()
    {
        _maxValues.Add(TileType.Desert, _boardLayout.GetValues().desert);
        _maxValues.Add(TileType.Brick, _boardLayout.GetValues().brick);
        _maxValues.Add(TileType.Ore, _boardLayout.GetValues().ore);
        _maxValues.Add(TileType.Grain, _boardLayout.GetValues().grain);
        _maxValues.Add(TileType.Wood, _boardLayout.GetValues().wood);
        _maxValues.Add(TileType.Wool, _boardLayout.GetValues().wool);
    }

    public TileType AddLandTile()
    {
        TileType tileType = TileType.Desert;
        var tryingToSet = true;

        var count = 0;

        while (tryingToSet)
        {
            tileType = (TileType) UnityEngine.Random.Range(0, 6);

            if (!_typeCount.ContainsKey(tileType))
            {
                _typeCount.Add(tileType, 1);
                tryingToSet = false;
            }

            if (_typeCount[tileType] == _maxValues[tileType])
            {
                count++;
                if (count == 100) tryingToSet = false;
                continue;
            }

            _typeCount[tileType]++;
            tryingToSet = false;
        }

        return tileType;
    }
}
