using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHarborDisplay : MonoBehaviour
{
    [SerializeField] private Sprite _brickSprite = null;
    [SerializeField] private Sprite _grainSprite = null;
    [SerializeField] private Sprite _oreSprite = null;
    [SerializeField] private Sprite _woodSprite = null;
    [SerializeField] private Sprite _woolSprite = null;

    public void SetSprite(TileType tileType)
    {
        var sprite = _woolSprite;

        switch (tileType)
        {
            case TileType.BrickHarbor:
                sprite = _brickSprite;
                break;

            case TileType.GrainHarbor:
                sprite = _grainSprite;
                break;

            case TileType.OreHarbor:
                sprite = _oreSprite;
                break;

            case TileType.WoodHarbor:
                sprite = _woodSprite;
                break;

            case TileType.WoolHarbor:
                sprite = _woolSprite;
                break;
        }

        GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }
}
