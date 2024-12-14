using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelMenu : MonoBehaviour
{
    // Start next level
    public void NextLevel()
    {
        if (GameOverMenu.lastLevelName == "Level1") {
            SceneManager.LoadScene("Level2");
            Debug.Log("Level 1 complete");
        }
        else if (GameOverMenu.lastLevelName == "Level2") {
            SceneManager.LoadScene("Level3");
            Debug.Log("Level 2 complete");
        }
        else {
            SceneManager.LoadScene("AllLevelsCompleteScreen");
            Debug.Log("Last level complete");
        }
    }

    // Quit to main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Return to main menu screen");
    }
}
