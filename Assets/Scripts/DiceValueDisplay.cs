using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceValueDisplay : MonoBehaviour
{
    [SerializeField] private Color32 _highlightColor = new Color32();

    public void SetValue(int value)
    {
        var tmp = GetComponentInChildren<TextMeshProUGUI>();
        
        tmp.text = value.ToString();

        if (value == 6 || value == 8)
        {
            tmp.color = _highlightColor;
        }
    }
}
