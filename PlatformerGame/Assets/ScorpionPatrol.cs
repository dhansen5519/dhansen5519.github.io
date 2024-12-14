using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionPatrol : MonoBehaviour
{
    public float speed = 2f; // Speed of scorpion walking animation
    public Transform[] patrolPoints;  // list of patrol points
    private int currentPoint = 0;

    private SpriteRenderer spriteRenderer; 

    // Header is the information that will appear in inspector
    [Header("Collision Settings")]
    public LayerMask playerLayer; // Layer for the player
    public float topHitOffset = 0.1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Patrol();
    }

    // This function handles the patrol behavior of the scorpions.
    // It includes the patrol points for the scorpions patrol range.
    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        // Move towards the current patrol point
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPoint].position, speed * Time.deltaTime);

        // Check if scorpion has reached the patrol point
        if (Vector2.Distance(transform.position, patrolPoints[currentPoint].position) < 0.1f)
        {
            // Move to the next point
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
            FlipSprite();
        }
    }

    // Flips the direction of the scorpion sprite so it is facing the direction it is walking
    void FlipSprite()
    {
        // Check direction the scorpion is moving
        Vector2 direction = patrolPoints[currentPoint].position - transform.position;

        // Flip based on x direction
        if (direction.x > 0 && !spriteRenderer.flipX) // Moving right
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x < 0 && spriteRenderer.flipX) // Moving left
        {
            spriteRenderer.flipX = false;
        }
    }

    // This function detects when the player comes in contact wit the scorpion.
    // If the player hits the front or back of the scorpion it will trigger character death.
    // If the player hits the top of the scorpion the scorpion game object will destruct.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object is the player
        if (collision.collider.CompareTag("Player"))
        {
            Vector2 collisionPoint = collision.GetContact(0).point; // Get point of contact
            Vector2 scorpionTop = new Vector2(transform.position.x, transform.position.y + topHitOffset); // Top of the scorpion

            if (collisionPoint.y > scorpionTop.y)
            {
                // Player hit the scorpion from above
                Destroy(gameObject); // Destroy the scorpion
                Debug.Log("Player killed scorpion");
            }
            else {
                // Player hit the scorpion from the side
                // Call players die method
                collision.collider.GetComponent<PlayerController>().Die();
                Debug.Log("Scorpion killed player");
            }
        }
    }
}
