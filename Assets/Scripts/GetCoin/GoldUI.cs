using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using System;


public class GoldUI : MonoBehaviour
{
    private TextMeshProUGUI goldText;

    private void Awake()
    {
        goldText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // Ensure CurrencyManager exists
        if (CurrencyManager.Instance != null)
        {
            // Subscribe to the gold changed event
            CurrencyManager.Instance.OnGoldChanged.AddListener(UpdateGoldText);
            
            // Update immediately so UI shows current gold
            UpdateGoldText(CurrencyManager.Instance.GetGold());
        }
        else
        {
            Debug.LogWarning("CurrencyManager instance not found!");
        }
    }

    private void OnDestroy()
    {
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.OnGoldChanged.RemoveListener(UpdateGoldText);
    }

    private void UpdateGoldText(int currentGold)
    {
        goldText.text = currentGold.ToString();
    }
}
