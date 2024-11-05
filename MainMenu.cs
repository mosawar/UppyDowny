using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Method to start the game by loading the next scene in the build index
    public void PlayGame()
    {
        // Loads the next scene in the build order, assuming the main game level follows the main menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Method to go to the settings menu scene
    public void GoToSettingsMenu()
    {
        // Loads the "SettingsMenu" scene by name, which should be included in the build settings
        SceneManager.LoadScene("SettingsMenu");
    }

    // Method to return to the main menu scene
    public void GoToMainMenu()
    {
        // Loads the "MainMenu" scene by name, allowing navigation back to the main menu
        SceneManager.LoadScene("MainMenu");
    }

    // Method to quit the game application
    public void QuitGame()
    {
        // Exits the application; note that this will not function in the editor, only in a built executable
        Application.Quit();
    }
}
