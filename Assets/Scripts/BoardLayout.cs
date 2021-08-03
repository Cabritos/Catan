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

    [SerializeField] private int[] _diceValues = new []{8, 3, 6, 2, 5, 10, 8, 4, 11, 12, 9, 10, 5, 4, 9, 11, 3, 6};

    public int[] GetDiceValues => _diceValues;

    public (int desert, int brick, int grain, int ore, int wood, int wool) GetValues()
    {
        return (_desert, _brick, _grain, _ore, _wood, _wool);
    }
}
