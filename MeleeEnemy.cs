using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    // Fields to control enemy attack behavior
    [SerializeField] private float attackCooldown;      // Time between attacks
    [SerializeField] private float range;               // Range within which the enemy can detect the player
    [SerializeField] private float colliderDistance;    // Distance to adjust the collider for detecting the player
    [SerializeField] private int damage;                // Damage dealt to the player on attack
    [SerializeField] private BoxCollider2D boxCollider; // Box collider to detect the player's presence
    [SerializeField] private LayerMask playerLayer;     // Layer to specify which objects are players

    // Timer to track cooldown between attacks
    private float cooldownTimer = Mathf.Infinity;

    // References to other components
    private Animator anim;                   // Animator to control enemy animations
    private PlayerHealth playerHealth;       // Reference to the player's health component to deal damage
    private AudioManager audioManager;       // Reference to AudioManager to play attack sounds

    private void Awake()
    {
        // Initialize references
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        // Increase cooldown timer by the time passed since the last frame
        cooldownTimer += Time.deltaTime;

        // Check if player is in sight and attack cooldown has elapsed
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                // Reset cooldown and trigger attack animation and sound effect
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
                audioManager.PlaySFX(audioManager.crowAttack);
            }
        }
    }

    // Checks if the player is within attack range using a boxcast
    // Credit to @Pandemonium on YouTube
    private bool PlayerInSight()
    {
        // Casts a box to detect the player in a specified range in front of the enemy
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
            0, Vector2.left, 0, playerLayer);
        
        // If a player is detected, reference their health component
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<PlayerHealth>();
        }
        return hit.collider != null;
    }
    
    // Visual representation of the detection box in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // Method called during attack animation to deal damage to the player
    private void DamagePlayer()
    {
        // Checks again if the player is within range before dealing damage
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
