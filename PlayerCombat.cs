using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Fields to control player combat
    public Animator animator;                  // Reference to the player's animator for triggering attack animations
    public Transform attackPoint;              // Position from which the player attacks (usually near the weapon or hand)
    public float attackRange = 0.5f;           // Radius of the attack area
    public LayerMask enemyLayers;              // Layer mask to specify what objects are considered enemies
    public int attackDamage = 1;               // Damage dealt per attack
    public float attackRate = 2f;              // Attack rate (attacks per second)
    private float nextAttackTime = 0f;         // Time when the player can perform the next attack

    private AudioManager audioManager;         // Reference to the AudioManager to play attack sounds

    private void Awake()
    {
        // Initialize the AudioManager reference by finding the GameObject tagged "Audio"
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if enough time has passed since the last attack to allow a new attack
        if (Time.time >= nextAttackTime)
        {
            // Check if the attack key (G) is pressed
            if (Input.GetKeyDown(KeyCode.G))
            {
                // Perform attack and set the cooldown for the next attack
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    // Method to handle attacking behavior
    void Attack()
    {
        // Trigger the attack animation
        animator.SetTrigger("Attack");

        // Detect all enemies within attack range using OverlapCircleAll
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Play the sword attack sound effect
        audioManager.PlaySFX(audioManager.sword);

        // Loop through all hit enemies and apply damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<enemyPatrol>().TakeDamage(attackDamage);
        }
    }

    // Visualize the attack range in the Unity Editor for easier positioning and tuning
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        // Draw a wire sphere at the attack point to indicate the attack range
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
