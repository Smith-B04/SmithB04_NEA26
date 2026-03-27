using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenButton : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textBox.text = "SCORE: " + (PlayerPrefs.GetInt("Score")).ToString();
        this.GetComponent<PlayerPrefScript>().ResetPrefs();   
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
