using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Function to load Level 1
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1"); 
    }
}
