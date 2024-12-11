using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Get the Animator component on the same GameObject
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with the chest
        if (collision.CompareTag("Player"))
        {
            // Trigger the "Open" animation
            animator.SetTrigger("Open");

            // Load the next level menu after the chest opens
            Invoke("OpenNextLevelMenu", 1f); // Adjust delay for animation
        }
    }

    private void OpenNextLevelMenu()
    {
        // Load the Next Level Menu
        UnityEngine.SceneManagement.SceneManager.LoadScene("NextLevelMenu");
    }
}
