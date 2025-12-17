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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
