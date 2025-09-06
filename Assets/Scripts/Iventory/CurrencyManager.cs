using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField] private int playerGold = 0; // Starting gold
    
    public UnityEvent<int> OnGoldChanged; // Event to notify UI/store about gold changes

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
        // Try to connect to GoldInventory on the player
        GoldInventory goldInventory = FindObjectOfType<GoldInventory>();
        if (goldInventory != null)
        {
            goldInventory.OnCoinCollected.AddListener(UpdateGoldFromInventory);
        }

        // Fire initial event so UI/store starts with correct value
        OnGoldChanged.Invoke(playerGold);
    }

    private void UpdateGoldFromInventory(GoldInventory inventory)
    {
        AddGold(10); // since each coin pickup gives +10 in GoldInventory
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
            Debug.Log($"Spent {amount} gold. Remaining: {playerGold}");
            OnGoldChanged.Invoke(playerGold); // notify UI/store
            return true;
        }
        Debug.Log("Not enough gold!");
        return false;
    }

    public void AddGold(int amount)
    {
        playerGold += amount;
        Debug.Log($"Added {amount} gold. Total: {playerGold}");
        OnGoldChanged.Invoke(playerGold); // notify UI/store
    }
}

/*
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; set; }

    [SerializeField] private int playerGold = 2000; // Αρχικά χρήματα - αυξημένα για τα νέα όπλα

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
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
            Debug.Log($"Spent {amount} gold. Remaining: {playerGold}");
            return true;
        }
        Debug.Log("Not enough gold!");
        return false;
    }

    public void AddGold(int amount)
    {
        playerGold += amount;
        Debug.Log($"Added {amount} gold. Total: {playerGold}");
    }
} 
*/