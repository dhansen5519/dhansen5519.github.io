using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb; 
    private Animator animator;

    [Header("Movement")]   // Header attribute relates to the inspector in unity and keeps groups organized
    public float moveSpeed = 5f; // Speed at which the player moves horizontally
    public float jumpForce = 5f; // Force applied when the player jumps

    [Header("Ground Check")]
    public Transform groundCheck; // Empty GameObject used to check if the player is grounded
    public float groundCheckRadius = 0.2f; // Radius of the ground check area
    public LayerMask groundLayer; // LayerMask to specify which layers count as ground
    //public LayerMask waterLayer; // LayerMask to specify the water layer

    // State variables
    private bool isGrounded;      // Whether the player is currently on the ground
    private bool isFacingRight = true; // Whether the player is facing right (true) or left (false)
    private bool isDead = false; // Whether the player is dead
    public float deathDelay = 2f; // Dictates how fast the death animation performs


    void Start()
    {
        // Get references to Rigidbody2D and Animator components on the same GameObject
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return; // Prevent movement if the player is dead
        // Call movement and jump methods every frame
        Move();
        Jump();
        UpdateAnimation();

    }

    // Move: handles horizontal movement using Unity’s Input.GetAxis("Horizontal")
    // Returns values between -1 and 1 based on player input (left/right arrow keys)
    private void Move()
    {
        // Get horizontal input
        float moveInput = Input.GetAxis("Horizontal");

        // Set the velocity based on input and moveSpeed preserving vertical velocity
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip character's direction based on movement direction
        if (moveInput > 0 && !isFacingRight)
            Flip();
        else if (moveInput < 0 && isFacingRight)
            Flip();
    }

    // Jump() checks if the character is on the ground using Physics2D.OverlapCircle.
    // This detects colliders within a specified radius. When grounded pressing the 
    // jump button (space bar) applies an upward force to rb.velocity which makes the character jump.
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

    public void Die()
    {
        if (isDead) return; // Prevent multiple triggers
        isDead = true;

        // Trigger the "Dead" animation
        animator.SetTrigger("Dead");

        // Stop all movement
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // Makes the Rigidbody static
        rb.simulated = false; // Disables Rigidbody physics

        StartCoroutine(LoadGameOverMenu());

    }

    // Checks if the player collides with water which will trigger death
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Debug.Log("Player touched water"); // debugging test
            Die();
        }
    }

    // IEnumerator used with coroutines to introduce delays. In this case it allows the 
    // death animation to occur befor the Game Over Menu is triggered
    private IEnumerator LoadGameOverMenu() 
    {
        // Store the current level name in the static variable for the replay game option
        GameOverMenu.lastLevelName = SceneManager.GetActiveScene().name;
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene("GameOverMenu");
    }
}
