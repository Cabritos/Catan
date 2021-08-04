using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private GameObject _roadSlotPrefab = null;
    [SerializeField] LayerMask _layerMask;

    private Dictionary<SettlementSpot, BoxCollider2D> _roadSpots = new Dictionary<SettlementSpot, BoxCollider2D>();
    private BoardTile[] _boardTiles;

    void Awake()
    {
        _boardTiles = GetComponentsInChildren<BoardTile>();
    }
    
    public void SetRoadSpots()
    {
        var contactFilter = new ContactFilter2D();
            contactFilter.useTriggers = false;
            contactFilter.SetLayerMask(_layerMask);
            contactFilter.useLayerMask = true;
        
        foreach (var boardTile in _boardTiles)
        {
            if (!boardTile.IsLand) continue;

            var polygon = boardTile.GetComponent<PolygonCollider2D>();
            var vectors = polygon.GetPath(0);

            for (int i = 0; i < polygon.GetTotalPointCount(); i++)
            {
                var firstVector = vectors[i];

                var secondIndex = i == polygon.GetTotalPointCount() - 1 ? 0 : i + 1;
                var secondVector = vectors[secondIndex];
                
                var midpoint = new Vector2(
                    (firstVector.x + secondVector.x) / 2,
                    (firstVector.y + secondVector.y) / 2);

                //I'm not proud of this
                var rescale = true;
                float rotation = 0;
                switch (i)
                {
                    case 0:
                        rotation = -72.55f;
                        break;

                    case 1:
                        rotation = 0f;
                        rescale = false;
                        break;

                    case 2:
                        rotation = 72.55f;
                        break;

                    case 3:
                        rotation = -72.55f;
                        break;

                    case 4:
                        rotation = 0f;
                        rescale = false;
                        break;

                    case 5:
                        rotation = 72.55f;
                        break;
                }

                var candidate = Instantiate(
                    _roadSlotPrefab,
                    boardTile.transform.TransformPoint(midpoint),
                    Quaternion.identity,
                boardTile.transform);

                candidate.transform.Rotate(0, 0, rotation);

                if (rescale) candidate.transform.localScale += new Vector3(0, 0.5f,0);

                var collides = false; 
                var collider = candidate.GetComponent<BoxCollider2D>();

                BoxCollider2D[] results = new BoxCollider2D[10]; 
                collider.OverlapCollider(contactFilter, results);
                
                foreach (var result in results)
                    if (result != null) collides = true;

                if (collides) 
                { 
                    Destroy(candidate); 
                    continue;
                }

                _roadSpots.Add(candidate.GetComponent<SettlementSpot>(), collider);
                Debug.Log(_roadSpots.Count);
            }
        }
    }
}
