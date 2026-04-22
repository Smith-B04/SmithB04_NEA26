//Created: Sprint 6
//Last Edited: Sprint 6
//Purpose: Control the death screen.

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenButton : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textBox.text = "SCORE: " + (PlayerPrefs.GetInt("Score")).ToString(); // display score
        this.GetComponent<PlayerPrefScript>().ResetPrefs();   
    }

    // called by the button to load the main menu
    public void OnClick()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
