using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Variables to manage player's health
    public int playerMaxHealth;             // Maximum health of the player
    public int playerCurrentHealth;         // Current health of the player

    public Animator anim;                   // Animator component to trigger hurt animations
    public Timer time;                      // Reference to a Timer object to track or reset game time

    // Start is called before the first frame update
    void Start()
    {
        // Set player's current health to the maximum health at the start of the game
        playerCurrentHealth = playerMaxHealth;
    }

    // Method to handle player damage
    public void TakeDamage(int damage)
    {
        // Reduce current health by the amount of damage taken
        playerCurrentHealth -= damage;

        // Trigger the "hurt" animation
        anim.SetTrigger("playerHurt");

        // Check if the player's health has dropped to zero or below
        if (playerCurrentHealth <= 0)
        {
            // Deactivate player temporarily and reset their position
            gameObject.SetActive(false);
            gameObject.transform.position = new Vector2(-5.232f, 3.12f);

            // Reset player's health to the maximum value
            playerCurrentHealth = playerMaxHealth;

            // Reactivate player and reset timer
            gameObject.SetActive(true);
            time.elapsedTime = 0f;
        }
    }
}
