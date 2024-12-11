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
        // Debugging test
        Debug.Log("Reloading scene: " + lastLevelName);
        // Testing button functionality loads correct screen by hardcoding level name
        //lastLevelName = "Level1";
        SceneManager.LoadScene(lastLevelName); // Load the stored level name
    }

    // Function to quit the game
    public void QuitToMain()
    {
       SceneManager.LoadScene("MainMenu");
    }
}
