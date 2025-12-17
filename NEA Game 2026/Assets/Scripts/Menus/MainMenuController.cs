//Created: Sprint 5
//Last Edited: Sprint 5
//Purpose: Main Menu Buttons
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Canvas settings;

    void Start()
    {
        if (PlayerPrefs.GetInt("NewGameStarted") == 0)
        {
            this.GetComponent<PlayerPrefScript>().ResetPrefs();
            PlayerPrefs.SetInt("NewGameStarted", 1);
        }
    }

    public void play()
    {
        SceneManager.LoadScene("Levels Menu");
    }

    public void Settings()
    {
        settings.enabled = true;
    }
}
