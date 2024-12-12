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
        // Debugging tests
        Debug.Log("Reloading scene: " + lastLevelName);
        // Testing button functionality loads correct screen by hardcoding level name
        // These hardcode name sets are used to test the buttons on the menu
        //lastLevelName = "Level1";
        //lastLevelName = "Level2";
        //lastLevelName = "Level3";

        SceneManager.LoadScene(lastLevelName); // Load the stored level name
    }

    // Function to quit to main menu
    public void QuitToMain()
    {
       SceneManager.LoadScene("MainMenu");
    }
}
