using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Gold : MonoBehaviour
{
    public AudioClip CoinSound; // The coin sound clip
    private void OnTriggerEnter(Collider other)
    {
        // Get the GoldInventory component from the other GameObject
        GoldInventory goldInventory = other.GetComponent<GoldInventory>();

        // If the other object has a GoldInventory component
        if (goldInventory != null)
        {
            // Call the CoinsCollected method to update the inventory
            goldInventory.CoinsCollected();

            // Play the sound at the position of the coin
            AudioSource.PlayClipAtPoint(CoinSound, transform.position);

            // Deactivate the coin (this object)
            gameObject.SetActive(false);
        }
    }
}
