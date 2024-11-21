using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
     public static string lastLevelName; // Static variable to store the level name

    // Function to retry the level
    public void RetryLevel()
    {
        Debug.Log("Reloading scene: " + lastLevelName);
        SceneManager.LoadScene(lastLevelName); // Load the stored level name
    }

    // Function to quit the game
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();

        // Note: Application.Quit() only works in a built application. In the editor, you can stop play mode with this:
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
