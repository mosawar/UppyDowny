using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter; // Stores reference to the active teleporter the player can use

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses the 'E' key to teleport
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Teleport the player if they are in range of a teleporter
            if (currentTeleporter != null)
            {
                // Set player position to the destination of the current teleporter
                transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
            }
        }
    }

    // Triggered when player enters the collider of a teleporter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider has the "Teleporter" tag
        if (collision.CompareTag("Teleporter"))
        {
            // Store the teleporter as the currentTeleporter
            currentTeleporter = collision.gameObject;
        }
    }

    // Triggered when player exits the collider of a teleporter
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the exiting collider is the current teleporter
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                // Clear the currentTeleporter reference when player leaves its range
                currentTeleporter = null;
            }
        }
    }
}
