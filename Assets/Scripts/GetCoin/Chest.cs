using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    public ParticleSystem shineFX; // Direct reference to the ParticleSystem
    private bool isOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (shineFX != null)
        {
            shineFX.Stop(); // Ensure it doesn't play on start
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            animator.SetBool("Open", true);
            isOpened = true;

            // Play the FX after a short delay to match animation
            StartCoroutine(PlayShineFXAfterDelay(1.0f)); // adjust delay to match animation
        }
    }

    IEnumerator PlayShineFXAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (shineFX != null)
        {
            shineFX.Play();
        }
    }
}
