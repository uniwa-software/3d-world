using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
public class GoldInventory : MonoBehaviour
{
    public int NumberOfCoins { get; private set; }

    public UnityEvent<GoldInventory> OnCoinCollected;
    public void CoinsCollected()
    {
        NumberOfCoins += 10;
        OnCoinCollected.Invoke(this);
    }
    
        public void AddCoins()
    {
        NumberOfCoins += 500;
        OnCoinCollected.Invoke(this);
    }

}
