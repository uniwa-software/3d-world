using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;

    public GoldInventory goldInventory;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            animator.SetBool("Open", true);
            isOpened = true;

            if (goldInventory != null)
            {
                goldInventory.AddCoins();
            }
        }
    }
}
