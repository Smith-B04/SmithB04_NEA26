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
        this.GetComponent<PlayerPrefScript>().ResetPrefs();
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
