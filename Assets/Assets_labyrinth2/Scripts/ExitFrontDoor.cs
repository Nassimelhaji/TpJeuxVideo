using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFrontDoor : MonoBehaviour
{
    public bool CanOpen = true; // Door starts open
    private int keysCollected = 0; // Track collected keys
    private Animator doorAnimator;

    private void Start()
    {
        // Get the Animator component attached to this GameObject
        doorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            if (CanOpen)
            {
                // Close the door when the player enters
                doorAnimator.SetTrigger("Close");
                CanOpen = false;
                Debug.Log("The door is closing.");
            }
            else if (keysCollected >= 3)
            {
                // Reopen the door if the player has collected 3 keys
                doorAnimator.SetTrigger("Open");
                CanOpen = true;
                Debug.Log("The door is reopening.");
            }
        }
    }

    // Call this method when a key is collected
    public void CollectKey()
    {
        keysCollected++;
        Debug.Log("Keys collected: " + keysCollected);
    }
}
