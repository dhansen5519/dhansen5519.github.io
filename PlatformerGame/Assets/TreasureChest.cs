using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureChest : MonoBehaviour
{
    private Animator animator;
    public float openDelay = 1f;

    private void Start()
    {
        // Get the Animator component on the same GameObject
        animator = GetComponent<Animator>();
    }

    // Triggers opening next level menu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with the chest
        if (collision.CompareTag("Player"))
        {
            // Trigger the "Open" animation
            animator.SetTrigger("Open");

            // Load the next level menu after the chest opens
            StartCoroutine(LoadNextLevelMenu());
        }
    }

    private IEnumerator LoadNextLevelMenu() 
    {
        // Store the current level name in the static variable for the replay game option
        GameOverMenu.lastLevelName = SceneManager.GetActiveScene().name;
        yield return new WaitForSeconds(openDelay);
        SceneManager.LoadScene("NextLevelMenu");
    }
}
