using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class WalkingSounds : MonoBehaviour
{
    public AudioSource WalkingSound;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            WalkingSound.enabled = true;
        }
        else
        {
            WalkingSound.enabled = false;
        }
    }
}
