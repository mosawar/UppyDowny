using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player movement attributes
    public float acceleration;               // Controls how fast the player accelerates when moving
    public float groundSpeed;                // Maximum speed the player can reach while on the ground
    public float jumpSpeed;                  // Speed at which the player jumps
    [Range(0f, 1f)] 
    public float groundDecay;                // Deceleration factor applied when player is grounded and not moving
    public Rigidbody2D body;                 // Reference to the player's Rigidbody2D component for physics calculations
    public BoxCollider2D groundCheck;        // Collider used to check if the player is grounded
    public LayerMask groundMask;             // Defines which layers are considered "ground"

    public bool grounded;                    // Checks if the player is touching the ground
    float xInput;                            // Horizontal input value
    float yInput;                            // Vertical input value

    Animator animator;                       // Reference to the Animator component to trigger animations

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();     // Capture player input
        HandleJump();   // Handle jumping mechanics
    }

    // FixedUpdate is called on a fixed interval (useful for physics calculations)
    void FixedUpdate()
    {
        CheckGround();       // Check if the player is on the ground
        ApplyFriction();     // Apply friction when player is grounded
        MoveWithInput();     // Move player horizontally based on input

        // Update animator's xVelocity parameter based on player's speed
        animator.SetFloat("xVelocity", Math.Abs(body.velocity.x));
    }

    // Method to capture player input
    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrows)
        yInput = Input.GetAxis("Vertical");   // Get vertical input (W/S or Up/Down arrows)
    }

    // Method to move player horizontally based on input
    void MoveWithInput()
    {
        // Only apply movement if there is horizontal input
        if (Mathf.Abs(xInput) > 0)
        {
            // Calculate the new x velocity
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(body.velocity.x + increment, -groundSpeed, groundSpeed);
            body.velocity = new Vector2(newSpeed, body.velocity.y);

            // Flip player sprite to face the direction of movement
            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    // Method to handle player jump
    void HandleJump()
    {
        // Check if Jump button is pressed and player is grounded
        if (Input.GetButtonDown("Jump") && grounded)
        {
            // Set y velocity to jumpSpeed, allowing the player to jump
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }

    // Method to check if the player is touching the ground
    void CheckGround()
    {
        // Check if any colliders in groundMask are within the bounds of groundCheck collider
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    // Method to apply friction when the player is grounded and not moving
    void ApplyFriction()
    {
        // Apply decay to the player's velocity when grounded and no horizontal input
        if (grounded && xInput == 0 && body.velocity.y <= 0)
        {
            body.velocity *= groundDecay;
        }
    }
}
