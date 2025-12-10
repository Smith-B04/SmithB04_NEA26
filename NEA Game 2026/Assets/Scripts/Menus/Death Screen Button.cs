using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
