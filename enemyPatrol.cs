using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public GameObject pointA; // Patrol start point
    public GameObject pointB; // Patrol end point
    private Rigidbody2D rb; // Rigidbody for controlling movement
    public Animator anim; // Animator to handle enemy animations
    private Transform currentPoint; // Current destination point
    public float speed; // Patrol speed

    public int maxHealth; // Enemy's maximum health
    int currentHealth; // Enemy's current health

    AudioManager audioManager; // Reference to AudioManager for sound effects

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Set current health to max health at start
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform; // Start moving towards pointB
        anim.SetBool("isRunning", true); // Set running animation
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the enemy is still alive
        if (currentHealth >= 0)
        {
            // Calculate direction to current point
            Vector2 point = currentPoint.position - transform.position;
            
            // Set velocity to move in the direction of current point
            rb.velocity = currentPoint == pointB.transform ? new Vector2(speed, 0) : new Vector2(-speed, 0);

            // Check if the enemy is close enough to switch directions
            if (Vector2.Distance(transform.position, currentPoint.position) < 1f)
            {
                flip(); // Flip enemy's orientation
                audioManager.PlaySFX(audioManager.crow); // Play patrol sound
                currentPoint = (currentPoint == pointB.transform) ? pointA.transform : pointB.transform; // Switch destination
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize patrol points in the editor
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
    }

    // Flips the enemy to face the opposite direction
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Method to handle enemy taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hurt"); // Trigger hurt animation

        if (currentHealth <= 0)
        {
            StartCoroutine("WaitTime"); // Start death animation and coroutine to remove enemy
        }
    }

    // Coroutine to handle a delay before the enemy "dies"
    IEnumerator WaitTime()
    {
        anim.SetBool("isDead", true); // Set death animation
        yield return new WaitForSeconds(1f); // Wait for the animation to complete
        Die(); // Call Die method
    }

    // Method to disable the enemy and play death sound
    void Die()
    {
        audioManager.PlaySFX(audioManager.crowDeath); // Play death sound
        Debug.Log("Enemy Died!");
        gameObject.SetActive(false); // Deactivate enemy object
    }
}
