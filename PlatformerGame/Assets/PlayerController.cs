using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb; 
    private Animator animator;

    [Header("Movement")]
    public float moveSpeed = 5f; // Speed at which the player moves horizontally
    public float jumpForce = 10f; // Force applied when the player jumps

    [Header("Ground Check")]
    public Transform groundCheck; // Empty GameObject used to check if the player is grounded
    public float groundCheckRadius = 0.2f; // Radius of the ground check area
    public LayerMask groundLayer; // LayerMask to specify which layers count as "ground"

    // State variables
    private bool isGrounded;      // Whether the player is currently on the ground
    private bool isFacingRight = true; // Whether the player is facing right (true) or left (false)


    // Start is called before the first frame update
    void Start()
    {
        // Get references to Rigidbody2D and Animator components on the same GameObject
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Call movement and jump methods every frame
        Move();
        Jump();
        UpdateAnimation();

    }

    // Move: handles horizontal movement using Unity’s Input.GetAxis("Horizontal").
    // Returns values between -1 and 1 based on player input (left/right arrow keys or A/D keys).
    private void Move()
    {
        // Get horizontal input (left/right or A/D keys)
        float moveInput = Input.GetAxis("Horizontal");

        // Set the velocity based on input and moveSpeed, preserving vertical velocity
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip character's direction based on movement direction
        if (moveInput > 0 && !isFacingRight)
            Flip();
        else if (moveInput < 0 && isFacingRight)
            Flip();
    }

    // Jump() checks if the character is on the ground using Physics2D.OverlapCircle.
    // This detects colliders within a specified radius. When grounded, pressing the 
    //"Jump" button applies an upward force to rb.velocity which makes the character jump.
    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,groundLayer);

        // If the player is grounded and the jump button is pressed
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Set the vertical velocity to jumpForce to make the player jump
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Trigger the jump animation
            animator.SetTrigger("Jump");
        }
    }

    // This method updates animation parameters based on movement and grounded state
    private void UpdateAnimation()
    {
        // Set the "Speed" parameter to the absolute horizontal speed for running/idle animations
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        // Set the "isGrounded" parameter to control jump/fall animations
        animator.SetBool("isGrounded", isGrounded);
    }

    // Method to flip the character’s direction
    private void Flip()
    {
        // Toggle the facing direction
        isFacingRight = !isFacingRight;

        // Reverse the character's x scale to flip its direction
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
