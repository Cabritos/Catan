using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesDictionary : MonoBehaviour
{
    [SerializeField] private TileBase _desertTile = null;
    [SerializeField] private TileBase _brickTile = null;
    [SerializeField] private TileBase _grainTile = null;
    [SerializeField] private TileBase _oreTile = null;
    [SerializeField] private TileBase _woodTile = null;
    [SerializeField] private TileBase _woolTile = null;
    [SerializeField] private TileBase _water = null;

    private Dictionary<TileType, TileBase> _tilesDictionary = new Dictionary<TileType, TileBase>();

    public void GenerateDictionary()
    {
        _tilesDictionary.Add(TileType.Desert, _desertTile);
        _tilesDictionary.Add(TileType.Brick, _brickTile);
        _tilesDictionary.Add(TileType.Grain, _grainTile);
        _tilesDictionary.Add(TileType.Ore, _oreTile);
        _tilesDictionary.Add(TileType.Wool, _woodTile);
        _tilesDictionary.Add(TileType.Wood, _woolTile);
        _tilesDictionary.Add(TileType.Water, _water);
    }

    public TileBase GetTileBase(TileType tileType)
    {

        if (_tilesDictionary.ContainsKey(tileType))
        {
            return _tilesDictionary[tileType];
        }
        return _desertTile;
    }
}
