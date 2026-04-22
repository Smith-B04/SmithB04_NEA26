//Created: Sprint 5
//Last Edited: Sprint 5
//Purpose: Control the Main Menu Buttons

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Canvas settings;

    void Start()
    {
        // Ensure that player prefs are reset but not ever time the player loads the menu
        if (PlayerPrefs.GetInt("NewGameStarted") == 0)
        {
            this.GetComponent<PlayerPrefScript>().ResetPrefs();
            PlayerPrefs.SetInt("NewGameStarted", 1);
        }
    }

    // The method called by the button that takes you to the level's menu
    public void play()
    {
        SceneManager.LoadScene("Levels Menu");
    }

    // Method called by settings button
    public void Settings()
    {
        settings.enabled = true;
    }
}
