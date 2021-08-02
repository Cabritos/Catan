using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoardLayout : ScriptableObject
{
    [SerializeField] private int _desert = 0;
    [SerializeField] private int _brick = 0;
    [SerializeField] private int _grain = 0;
    [SerializeField] private int _ore = 0;
    [SerializeField] private int _wood = 0;
    [SerializeField] private int _wool = 0;

    public (int desert, int brick, int grain, int ore, int wood, int wool) GetValues()
    {
        return (_desert, _brick, _grain, _ore, _wood, _wool);
    }
}
