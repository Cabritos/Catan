using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadClone : MonoBehaviour
{
    [SerializeField] private GameObject _roadSlotPrefab = null;
    [SerializeField] LayerMask _layerMask;


    void Start()
    {
        SetSettlementSpots();
    }

    public void SetSettlementSpots()
    {
        var polygon = GetComponent<PolygonCollider2D>();
        var vectors = polygon.GetPath(0);

        for (int i = 0; i < polygon.GetTotalPointCount(); i++)
        {
            var firstVector = vectors[i];

            var secondIndex = i == polygon.GetTotalPointCount() - 1 ? 0 : i + 1;
            var secondVector = vectors[secondIndex];

            var midpoint = new Vector2(
                (firstVector.x + secondVector.x) / 2,
                (firstVector.y + secondVector.y) / 2);

            var angle = Vector2.Angle(secondVector, firstVector);

            Debug.Log(angle);

            var candidate = Instantiate(
                _roadSlotPrefab,
                transform.TransformPoint(midpoint),
                _roadSlotPrefab.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward),
                transform);
        }
    }
}
