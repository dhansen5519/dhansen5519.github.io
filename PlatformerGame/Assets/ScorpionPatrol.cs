using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionPatrol : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] patrolPoints;
    private int currentPoint = 0;

    private SpriteRenderer spriteRenderer; 

    // Logic for checking if the scorpion is hit from above
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
            }
            else {
                // Player hit the scorpion from the side
                // Call a method to kill the player (e.g., respawn or end game)
                collision.collider.GetComponent<PlayerController>().Die();
            }
        }
    }
}
