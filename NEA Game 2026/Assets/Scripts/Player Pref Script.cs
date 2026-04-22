//Created: Sprint 6
//Last Edited: Sprint 6
//Purpose: Holds the method to reset the player prefs.

using UnityEngine;

public class PlayerPrefScript : MonoBehaviour
{
    // method called when prefs need reseting
    public void ResetPrefs()
    {
        PlayerPrefs.SetInt("MaxHealth", 100);
        PlayerPrefs.SetInt("MaxStamina", 100);
        PlayerPrefs.SetInt("MaxFlasks", 3);
        PlayerPrefs.SetFloat("Physical", 1);
        PlayerPrefs.SetFloat("Fire", 1);
        PlayerPrefs.SetFloat("Magic", 1);
        PlayerPrefs.SetInt("SwordDamage", 15);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetString("LevelsBeaten", "");
        PlayerPrefs.SetInt("NewGameStarted", 0);
        PlayerPrefs.Save();
    }
}
