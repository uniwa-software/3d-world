using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isOpened)
        {
            GoldInventory goldInventory = other.GetComponent<GoldInventory>();

            if (goldInventory != null)
            {
                animator.SetBool("Open", true);
                isOpened = true;
                goldInventory.AddCoins();
            }
        }
    }
}