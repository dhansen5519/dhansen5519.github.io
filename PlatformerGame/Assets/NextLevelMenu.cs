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
        }
        else if (GameOverMenu.lastLevelName == "Level2") {
            SceneManager.LoadScene("Level3");
        }
        else {
            SceneManager.LoadScene("MainMenu");
            Debug.Log("Last level complete");
        }
    }

    // Quit to main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
