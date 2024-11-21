using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed to load new level

public class LevelComplete : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player entered the end point trigger
        if (collision.CompareTag("Player")) // Ensure the player GameObject has the "Player" tag
        {
            // Here, you can trigger the level completion process
            Debug.Log("Level Complete!");

            // Example: Load the next level or scene
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            // Alternatively, display a "Level Complete" message, stop player movement, etc.
            CompleteLevel();
        }
    }

    // Custom method for handling level completion
    private void CompleteLevel()
    {
        // Perform actions for level completion, such as displaying a message
        Debug.Log("Congratulations! You've completed the level.");
        // Additional code to display UI or transition to a new level can go here
    }
}
