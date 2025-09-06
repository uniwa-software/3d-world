using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Gold : MonoBehaviour
{
    public AudioClip CoinSound;
    
    private void OnTriggerEnter(Collider other)
    {
        GoldInventory goldInventory = other.GetComponent<GoldInventory>();

        if (goldInventory != null)
        {
            goldInventory.CoinsCollected();
            AudioSource.PlayClipAtPoint(CoinSound, transform.position);
            gameObject.SetActive(false);
        }
    }
}