using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [SerializeField] private BoardLayout _boardLayout = null;
    [SerializeField] private Tilemap _tilemap = null;
    [SerializeField] private GameObject _diceValueDisplayPrefab = null;
    [SerializeField] private GameObject _harborDisplayPrefab = null;
    [SerializeField] private GameObject _resourceHarborDisplayPrefab = null;

    private BoardTile[] _boardTiles;
    private Dictionary<TileType, int> _maxValues = new Dictionary<TileType, int>();
    private Dictionary<TileType, int> _typeCount = new Dictionary<TileType, int>();

    private int[] _diceValues;
    private int _landTiles;

    private TilesDictionary _tilesDictionary;
    private GridLayout _gridLayout;

    void Awake()
    {
        _boardTiles = GetComponentsInChildren<BoardTile>();
        _tilesDictionary = GetComponent<TilesDictionary>();
        _gridLayout = _tilemap.layoutGrid;
    }

    void Start()
    {
        TogglePositionId();

        SetMaxResourcesTilesValues();
        _tilesDictionary.GenerateDictionary();
        _diceValues = _boardLayout.GetDiceValues;
        SetTilemap();
    }

    private void SetTilemap()
    {
        int count = 0;

        foreach (var boardTile in _boardTiles)
        {
            boardTile.SetGridPosition(count, _gridLayout.WorldToCell(boardTile.transform.position));
            boardTile.SetWorldPosition(_gridLayout.CellToWorld(boardTile.GridPosition));
            count++;

            if (boardTile.IsLand)
            {
                AddTile(boardTile, SetLandTile());

                if (boardTile.TileType == TileType.Desert) continue;

                GameObject display = Instantiate(
                    _diceValueDisplayPrefab, 
                    boardTile.gameObject.transform,
                    false);

                display.GetComponent<DiceValueDisplay>().SetValue(_diceValues[_landTiles]); 
                boardTile.SetDiceValue(_diceValues[_landTiles]);
                _landTiles++;

                continue;
            }

            boardTile.SetHarbor();

            if (boardTile.IsResourceHarbor)
            {
                var display = Instantiate(
                    _resourceHarborDisplayPrefab,
                    boardTile.gameObject.transform,
                    false);

                AddTile(boardTile, SetHarborTile());
                display.GetComponent<ResourceHarborDisplay>().SetSprite(boardTile.TileType);

                continue;
            }

            if (boardTile.IsHarbor)
            {
                Instantiate(
                    _harborDisplayPrefab,
                    boardTile.gameObject.transform,
                    false);
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
            tileType = (TileType)UnityEngine.Random.Range(8, 13);

            if (!_typeCount.ContainsKey(tileType))
            {
                _typeCount.Add(tileType, 1);
                break;
            }

            count++;
            if (count == 100)
            {
                Debug.LogError("Error in harbor tiling ");
                break;
            }
        }

        return tileType;
    }

    public void TogglePositionId()
    {
        foreach (var boardTile in _boardTiles)
        {
            boardTile.GetComponentInChildren<TMP_Text>().enabled = !boardTile.GetComponentInChildren<TMP_Text>().enabled;
        }
    }
}
