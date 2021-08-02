using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BoardTile : MonoBehaviour
{
    [SerializeField] private Vector3Int _position;
    [SerializeField] private int _positionId = 0;
    [SerializeField] private TileType _tileType;
    [SerializeField] private bool _isLand;
    [SerializeField] private int? _diceValue;
    [SerializeField] private bool _hasRobber;
    [SerializeField] private TMP_Text _idText = null;

    public Vector3Int Position => _position;
    public TileType TileType => _tileType;
    public bool IsLand => _isLand;

    void Start()
    {
        _idText.text = _positionId.ToString();
    }

    public void SetTileType(TileType tileType)
    {
        _tileType = tileType;
    }
}