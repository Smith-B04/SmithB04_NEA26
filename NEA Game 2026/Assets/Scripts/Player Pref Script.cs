using UnityEngine;

public class PlayerPrefScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ResetPrefs()
    {
        PlayerPrefs.SetInt("MaxHealth", 100);
        PlayerPrefs.SetInt("MaxStamina", 100);
        PlayerPrefs.SetInt("MaxFlasks", 3);
        PlayerPrefs.SetFloat("Physical", 1);
        PlayerPrefs.SetFloat("Fire", 1);
        PlayerPrefs.SetFloat("Magic", 1);
        PlayerPrefs.SetInt("SwordDamage", 15);
        PlayerPrefs.SetString("LevelsBeaten", "");
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
