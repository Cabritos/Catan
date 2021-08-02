using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BoardTile : MonoBehaviour
{
    [SerializeField] private Vector3Int _gridPosition;
    [SerializeField] private int _positionId = 0;
    [SerializeField] private TileType _tileType;
    [SerializeField] private bool _isLand = false;
    [SerializeField] private int _diceValue;
    [SerializeField] private bool _hasRobber;
    [SerializeField] private bool _isHarbor;
    [SerializeField] private bool _isResourceHarbor;
    [SerializeField] private TMP_Text _idText = null;

    public Vector3Int GridPosition => _gridPosition;
    public int PositionId => _positionId;
    public TileType TileType => _tileType;
    public bool IsLand => _isLand;
    public bool IsHarbor => _isHarbor;
    public bool IsResourceHarbor => _isResourceHarbor;

    public void SetTileType(TileType tileType)
    {
        _tileType = tileType;
        _idText.text = _positionId.ToString();
    }


    public void SetPositionId(int positionId)
    {
        _positionId = positionId;
    }

    public void SetWorldsPosition(Vector3 worldPosition)
    {
        transform.position = worldPosition - new Vector3(0,0,1);
    }

    public void SetGridPosition(int positionId, Vector3Int gridPosition)
    {
        _gridPosition = gridPosition;
        _positionId = positionId;
    }

    public void SetWorldPosition(Vector3 worldPosition)
    {
        transform.position = worldPosition - new Vector3(0, 0, 1);
    }

    public void SetDiceValue(int diceValue)
    {
        _diceValue = diceValue;
    }

    public void SetHarbor()
    {
        if (_positionId % 4 == 0)
        {
            _isResourceHarbor = true;
            return;
        }

        if (_positionId % 2 == 0)
            _isHarbor = true;
    }
}