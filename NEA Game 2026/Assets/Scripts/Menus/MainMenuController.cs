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
            PlayerPrefs.SetInt("MaxHealth", 100);
            PlayerPrefs.SetInt("MaxStamina", 100);
            PlayerPrefs.SetInt("MaxFlasks", 3);
            PlayerPrefs.SetFloat("Physical", 1);
            PlayerPrefs.SetFloat("Fire", 1);
            PlayerPrefs.SetFloat("Magic", 1);
            PlayerPrefs.SetInt("SwordDamage", 15);
            PlayerPrefs.SetString("LevelsBeaten", "");
            PlayerPrefs.SetInt("NewGameStarted", 1);
            PlayerPrefs.Save();
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
