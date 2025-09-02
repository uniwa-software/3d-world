using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using System;

public class DoldUI : MonoBehaviour
{
    private TextMeshProUGUI GoldText;

    void Start()
    {
        GoldText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateGoldText(GoldInventory goldInventory)
    {
        GoldText.text = goldInventory.NumberOfCoins.ToString();
        
    }
}
