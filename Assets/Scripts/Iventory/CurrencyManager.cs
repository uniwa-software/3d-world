using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField] private int playerGold = 0;
    
    public UnityEvent<int> OnGoldChanged;
    
    private int lastKnownInventoryCoins = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            if (OnGoldChanged == null)
                OnGoldChanged = new UnityEvent<int>();
        }
    }

    private void Start()
    {
        GoldInventory goldInventory = FindObjectOfType<GoldInventory>();
        if (goldInventory != null)
        {
            goldInventory.OnCoinCollected.AddListener(SyncWithInventory);
            lastKnownInventoryCoins = goldInventory.NumberOfCoins;
        }

        OnGoldChanged.Invoke(playerGold);
    }

    private void SyncWithInventory(GoldInventory inventory)
    {
        int coinsAdded = inventory.NumberOfCoins - lastKnownInventoryCoins;
        lastKnownInventoryCoins = inventory.NumberOfCoins;
        
        AddGold(coinsAdded);
    }

    public int GetGold()
    {
        return playerGold;
    }

    public bool CanAfford(int amount)
    {
        return playerGold >= amount;
    }

    public bool SpendGold(int amount)
    {
        if (CanAfford(amount))
        {
            playerGold -= amount;
            OnGoldChanged.Invoke(playerGold);
            return true;
        }
        return false;
    }

    public void AddGold(int amount)
    {
        playerGold += amount;
        OnGoldChanged.Invoke(playerGold);
    }
}