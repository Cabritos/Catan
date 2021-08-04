using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementsManager : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab = null;
    [SerializeField] LayerMask _layerMask;

    private Dictionary<SettlementSpot, CircleCollider2D> _settlementSpots = new Dictionary<SettlementSpot, CircleCollider2D>();
    private BoardTile[] _boardTiles;

    void Awake()
    {
        _boardTiles = GetComponentsInChildren<BoardTile>();
    }

    public void SetSettlementSpots()
    {
        var contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(_layerMask);
        contactFilter.useLayerMask = true;

        foreach (var boardTile in _boardTiles)
        {
            if (!boardTile.IsLand) continue;

            foreach (var vector2 in boardTile.GetComponent<PolygonCollider2D>().points)
            {
                var candidate = Instantiate(
                    _slotPrefab,
                    boardTile.transform.TransformPoint(new Vector3(vector2.x, vector2.y, 0)),
                    Quaternion.identity,
                    boardTile.transform);

                var collides = false;
                var collider = candidate.GetComponent<CircleCollider2D>();

                Collider2D[] results = new Collider2D[10];
                collider.OverlapCollider(contactFilter, results);

                foreach (var result in results)
                {
                    if (result != null) collides = true;
                }

                if (collides)
                {
                    Destroy(candidate);
                    continue;
                }

                _settlementSpots.Add(candidate.GetComponent<SettlementSpot>(), collider);
            }
        }
    }
}
